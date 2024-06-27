namespace Demo3.Client.Models;

public record MeetupDetail(
    Guid Id,
    string Title,
    string Location,
    DateTime Date);
