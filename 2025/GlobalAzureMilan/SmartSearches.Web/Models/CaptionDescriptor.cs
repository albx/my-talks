namespace SmartSearches.Web.Models;

public record CaptionDescriptor
{
    public string Text { get; set; } = string.Empty;

    public string Highlights { get; set; } = string.Empty;
}
