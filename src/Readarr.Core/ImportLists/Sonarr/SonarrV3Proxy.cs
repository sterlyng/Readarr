using System;
using System.Collections.Generic;
using System.Net;
using FluentValidation.Results;
using Newtonsoft.Json;
using NLog;
using Readarr.Common.Extensions;
using Readarr.Common.Http;
using Readarr.Core.Localization;

namespace Readarr.Core.ImportLists.Readarr
{
    public interface IReadarrV3Proxy
    {
        List<ReadarrSeries> GetSeries(ReadarrSettings settings);
        List<ReadarrProfile> GetQualityProfiles(ReadarrSettings settings);
        List<ReadarrProfile> GetLanguageProfiles(ReadarrSettings settings);
        List<ReadarrRootFolder> GetRootFolders(ReadarrSettings settings);
        List<ReadarrTag> GetTags(ReadarrSettings settings);
        ValidationFailure Test(ReadarrSettings settings);
    }

    public class ReadarrV3Proxy : IReadarrV3Proxy
    {
        private readonly IHttpClient _httpClient;
        private readonly Logger _logger;
        private readonly ILocalizationService _localizationService;

        public ReadarrV3Proxy(IHttpClient httpClient, ILocalizationService localizationService, Logger logger)
        {
            _httpClient = httpClient;
            _localizationService = localizationService;
            _logger = logger;
        }

        public List<ReadarrSeries> GetSeries(ReadarrSettings settings)
        {
            return Execute<ReadarrSeries>("/api/v3/series", settings);
        }

        public List<ReadarrProfile> GetQualityProfiles(ReadarrSettings settings)
        {
            return Execute<ReadarrProfile>("/api/v3/qualityprofile", settings);
        }

        public List<ReadarrProfile> GetLanguageProfiles(ReadarrSettings settings)
        {
            return Execute<ReadarrProfile>("/api/v3/languageprofile", settings);
        }

        public List<ReadarrRootFolder> GetRootFolders(ReadarrSettings settings)
        {
            return Execute<ReadarrRootFolder>("api/v3/rootfolder", settings);
        }

        public List<ReadarrTag> GetTags(ReadarrSettings settings)
        {
            return Execute<ReadarrTag>("/api/v3/tag", settings);
        }

        public ValidationFailure Test(ReadarrSettings settings)
        {
            try
            {
                GetSeries(settings);
            }
            catch (HttpException ex)
            {
                if (ex.Response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _logger.Error(ex, "API Key is invalid");
                    return new ValidationFailure("ApiKey", _localizationService.GetLocalizedString("ImportListsValidationInvalidApiKey"));
                }

                if (ex.Response.HasHttpRedirect)
                {
                    _logger.Error(ex, "Readarr returned redirect and is invalid");
                    return new ValidationFailure("BaseUrl", _localizationService.GetLocalizedString("ImportListsReadarrValidationInvalidUrl"));
                }

                _logger.Error(ex, "Unable to connect to import list.");
                return new ValidationFailure(string.Empty, _localizationService.GetLocalizedString("ImportListsValidationUnableToConnectException", new Dictionary<string, object> { { "exceptionMessage", ex.Message } }));
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Unable to connect to import list.");
                return new ValidationFailure(string.Empty, _localizationService.GetLocalizedString("ImportListsValidationUnableToConnectException", new Dictionary<string, object> { { "exceptionMessage", ex.Message } }));
            }

            return null;
        }

        private List<TResource> Execute<TResource>(string resource, ReadarrSettings settings)
        {
            if (settings.BaseUrl.IsNullOrWhiteSpace() || settings.ApiKey.IsNullOrWhiteSpace())
            {
                return new List<TResource>();
            }

            var baseUrl = settings.BaseUrl.TrimEnd('/');

            var request = new HttpRequestBuilder(baseUrl).Resource(resource)
                .Accept(HttpAccept.Json)
                .SetHeader("X-Api-Key", settings.ApiKey)
                .Build();

            var response = _httpClient.Get(request);

            if ((int)response.StatusCode >= 300)
            {
                throw new HttpException(response);
            }

            var results = JsonConvert.DeserializeObject<List<TResource>>(response.Content);

            return results;
        }
    }
}
