using Microsoft.AspNetCore.Mvc.Abstractions;

namespace SmartSearches.Web.Models;

public record SearchResultItemModel
{
    public IndexDescriptor Document { get; init; } = default!;

    public double? Score { get; init; }

    public double? RerankerScore { get; init; }

    public CaptionDescriptor[]? Captions { get; init; }
}
