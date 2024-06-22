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
