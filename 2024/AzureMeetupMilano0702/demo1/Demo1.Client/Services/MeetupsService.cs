using Demo1.Shared;
using System.Net.Http.Json;

namespace Demo1.Client.Services;

public class MeetupsService
{
    private readonly HttpClient _httpClient;

    public MeetupsService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<MeetupListItem[]> GetAllMeetupsAsync()
    {
        var meetups = await _httpClient.GetFromJsonAsync<MeetupListItem[]>("api/meetups");
        return meetups ?? [];
    }

    public async Task<MeetupDetail?> GetMeetupDetailAsync(Guid meetupId)
    {
        try
        {
            var meetup = await _httpClient.GetFromJsonAsync<MeetupDetail>($"api/meetups/{meetupId}");
            return meetup;
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }
}
