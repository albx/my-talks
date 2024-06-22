namespace AzureMeetupMilanoSwa.Data.Models;

public class Meetup
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public DateTime Date { get; set; }
}
