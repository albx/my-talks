namespace Demo2.Shared;

public record MeetupListItem(
    Guid Id,
    string Title,
    string Location,
    DateTime Date);
