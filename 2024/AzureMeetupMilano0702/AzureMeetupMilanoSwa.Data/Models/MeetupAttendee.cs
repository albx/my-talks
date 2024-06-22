namespace AzureMeetupMilanoSwa.Data.Models;

public class MeetupAttendee
{
    public Guid Id { get; set; }

    public Guid MeetupId { get; set; }

    public string AttendeeId { get; set; } = string.Empty;

    public string AttendeeName { get; set; } = string.Empty;
}
