namespace Demo3.Client.Models;

public record MeetupListItem(
    Guid Id,
    string Title,
    string Location,
    DateTime Date);
