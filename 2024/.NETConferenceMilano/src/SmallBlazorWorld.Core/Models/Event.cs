namespace SmallBlazorWorld.Core.Models;

public class Event
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public DateOnly Date { get; set; }
}
