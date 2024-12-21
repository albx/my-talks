namespace SmallBlazorWorld.App.Shared.Models;

public record EventModel
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public DateOnly Date { get; init; }

    public string Location { get; init; } = string.Empty;
}
