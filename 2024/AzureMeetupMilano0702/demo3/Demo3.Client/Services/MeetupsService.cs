using Demo3.Client.Models;
using System.Net.Http.Json;

namespace Demo3.Client.Services;

public class MeetupsService
{
    private readonly HttpClient _httpClient;

    public MeetupsService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<MeetupListItem[]> GetAllMeetupsAsync()
    {
        var meetups = await _httpClient.GetFromJsonAsync<Response<MeetupListItem>>("Meetup");
        return meetups?.Value ?? [];
    }

    public async Task<MeetupDetail?> GetMeetupDetailAsync(Guid meetupId)
    {
        try
        {
            var meetup = await _httpClient.GetFromJsonAsync<Response<MeetupDetail>>($"Meetup/Id/{meetupId}");
            return meetup?.Value.FirstOrDefault();
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task<MeetupListItem[]> GetMyMeetupsAsync()
    {
        var meetups = await _httpClient.GetFromJsonAsync<Response<MeetupListItem>>("MyMeetups");
        return meetups?.Value ?? [];
    }

    public async Task AttendToMeetupAsync(Guid meetupId)
    {
        //var response = await _httpClient.PostAsJsonAsync("api/meetups/attend", new AttendToMeetupRequest(meetupId));
        //response.EnsureSuccessStatusCode();
        throw new NotImplementedException();
    }
}
