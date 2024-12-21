namespace SmallBlazorWorld.App.Shared.Models;

public record TalkModel
{
    public Guid Id { get; init; }

    public string Title { get; init; } = string.Empty;

    public string Abstract { get; init; } = string.Empty;
}