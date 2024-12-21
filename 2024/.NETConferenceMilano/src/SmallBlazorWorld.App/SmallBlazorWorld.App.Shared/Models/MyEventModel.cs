namespace SmallBlazorWorld.App.Shared.Models;

public record MyEventModel
{
    public string EventName { get; init; } = string.Empty;

    public string EventLocation { get; init; } = string.Empty;

    public DateOnly EventDate { get; init; }

    public string TalkTitle { get; init; } = string.Empty;
}
