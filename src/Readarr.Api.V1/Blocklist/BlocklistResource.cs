using System;
using System.Collections.Generic;
using Readarr.Api.V1.CustomFormats;
using Readarr.Api.V1.Series;
using Readarr.Core.CustomFormats;
using Readarr.Core.Indexers;
using Readarr.Core.Languages;
using Readarr.Core.Qualities;
using Readarr.Http.REST;

namespace Readarr.Api.V1.Blocklist;

public class BlocklistResource : RestResource
{
    public int SeriesId { get; set; }
    public List<int> EpisodeIds { get; set; } = new List<int>();
    public string SourceTitle { get; set; } = string.Empty;
    public List<Language> Languages { get; set; } = new List<Language>();
    public QualityModel Quality { get; set; } = new QualityModel();
    public List<CustomFormatResource> CustomFormats { get; set; } = new List<CustomFormatResource>();
    public DateTime Date { get; set; }
    public DownloadProtocol Protocol { get; set; }
    public string Indexer { get; set; }
    public string Message { get; set; }
    public string Source { get; set; }

    public SeriesResource Series { get; set; } = new SeriesResource();
}

public static class BlocklistResourceMapper
{
    public static BlocklistResource MapToResource(this Readarr.Core.Blocklisting.Blocklist model, ICustomFormatCalculationService formatCalculator)
    {
        return new BlocklistResource
        {
            Id = model.Id,
            SeriesId = model.SeriesId,
            EpisodeIds = model.EpisodeIds,
            SourceTitle = model.SourceTitle,
            Languages = model.Languages,
            Quality = model.Quality,
            CustomFormats = formatCalculator.ParseCustomFormat(model, model.Series).ToResource(false),
            Date = model.Date,
            Protocol = model.Protocol,
            Indexer = model.Indexer,
            Message = model.Message,
            Source = model.Source,
            Series = model.Series.ToResource()
        };
    }
}
