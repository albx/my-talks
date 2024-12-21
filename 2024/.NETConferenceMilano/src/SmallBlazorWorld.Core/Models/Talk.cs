namespace SmallBlazorWorld.Core.Models;

public class Talk
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Abstract { get; set; } = string.Empty;
}
