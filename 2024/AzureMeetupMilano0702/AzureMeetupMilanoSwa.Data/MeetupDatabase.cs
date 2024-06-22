using AzureMeetupMilanoSwa.Data.Models;
using Dapper;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace AzureMeetupMilanoSwa.Data;

public class MeetupDatabase
{
    private readonly MeetupDatabaseOptions _options;

    public MeetupDatabase(IOptions<MeetupDatabaseOptions> options)
    {
        _options = options.Value;
    }

    public async Task<IEnumerable<Meetup>> GetAllMeetupsAsync()
    {
        using var connection = BuildConnection();

        var meetups = await connection.QueryAsync<Meetup>("SELECT * FROM Meetups ORDER BY Date");
        return meetups;
    }

    public async Task<Meetup?> GetMeetupByIdAsync(Guid meetupId)
    {
        using var connection = BuildConnection();

        var meetup = await connection.QueryFirstOrDefaultAsync<Meetup>("SELECT * FROM Meetups WHERE Id=@meetupId", new { meetupId });
        return meetup;
    }

    public async Task<IEnumerable<MeetupAttendee>> GetAttendeesByMeetup(Guid meetupId)
    {
        using var connection = BuildConnection();

        var attendees = await connection.QueryAsync<MeetupAttendee>("SELECT * FROM MeetupAttendees WHERE MeetupId=@meetupId", new { meetupId });
        return attendees;
    }

    public async Task<IEnumerable<Meetup>> GetMeetupsByAttendeeAsync(string attendeeId)
    {
        using var connection = BuildConnection();

        var meetups = await connection.QueryAsync<Meetup>(
            "SELECT m.* FROM Meetups m JOIN MeetupAttendees ma ON m.Id=ma.MeetupId WHERE ma.AttendeeId=@attendeeId ORDER BY m.Date",
            new { attendeeId });

        return meetups;
    }

    public async Task AttendToMeetup(Guid meetupId, string attendeeId, string attendeeName)
    {
        using var connection = BuildConnection();

        await connection.ExecuteAsync(
            "INSERT INTO MeetupAttendees(Id, MeetupId, AttendeeId, AttendeeName) VALUES(@Id, @MeetupId, @AttendeeId, @AttendeeName)",
            new MeetupAttendee { Id = Guid.NewGuid(), MeetupId = meetupId, AttendeeId = attendeeId, AttendeeName = attendeeName });
    }

    private SqlConnection BuildConnection()
    {
        var connection = new SqlConnection(_options.ConnectionString);
        connection.Open();

        return connection;
    }

    public record MeetupDatabaseOptions
    {
        public string ConnectionString { get; set; } = string.Empty;
    }
}
