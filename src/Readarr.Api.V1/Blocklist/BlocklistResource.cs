using Readarr.Core.CustomFormats;
using Readarr.Core.Indexers;
using Readarr.Core.Languages;
using Readarr.Core.Qualities;
using Readarr.Api.V5.CustomFormats;
using Readarr.Api.V5.Series;
using Readarr.Http.REST;

namespace Readarr.Api.V5.Blocklist;

public class BlocklistResource : RestResource
{
    public int SeriesId { get; set; }
    public required List<int> EpisodeIds { get; set; }
    public required string SourceTitle { get; set; }
    public required List<Language> Languages { get; set; }
    public required QualityModel Quality { get; set; }
    public required List<CustomFormatResource> CustomFormats { get; set; }
    public DateTime Date { get; set; }
    public DownloadProtocol Protocol { get; set; }
    public string? Indexer { get; set; }
    public string? Message { get; set; }
    public string? Source { get; set; }

    public required SeriesResource Series { get; set; }
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
