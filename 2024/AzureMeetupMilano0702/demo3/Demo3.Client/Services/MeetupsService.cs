using Demo3.Client.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;

namespace Demo3.Client.Services;

public class MeetupsService
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public MeetupsService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _authenticationStateProvider = authenticationStateProvider ?? throw new ArgumentNullException(nameof(authenticationStateProvider));
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
        var authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var attendeeId = authenticationState.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var attendeeName = authenticationState.User.FindFirst(ClaimTypes.Name)?.Value;

        var response = await _httpClient.PostAsJsonAsync("AttendToMeetup", new { attendeeId, attendeeName, meetupId });
        response.EnsureSuccessStatusCode();
    }
}
