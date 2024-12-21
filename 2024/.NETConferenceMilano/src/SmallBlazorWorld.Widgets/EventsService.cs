using SmallBlazorWorld.App.Shared.Models;
using SmallBlazorWorld.App.Shared.Services;
using System.Net.Http.Json;

namespace SmallBlazorWorld.Widgets;

public class EventsService(HttpClient httpClient) : IEventsService
{
    public HttpClient HttpClient { get; } = httpClient;

    public Task<EventModel[]> GetEventsAsync()
    {
        throw new NotSupportedException();
    }

    public async Task<MyEventModel[]> GetMyEventsAsync()
    {
        var myEvents = await HttpClient.GetFromJsonAsync<MyEventModel[]>("api/my-events");
        return myEvents ?? [];
    }

    public Task<TalkModel[]> GetTalksAsync()
    {
        throw new NotSupportedException();
    }

    public Task SubmitProposalAsync(SubmitProposalModel model)
    {
        throw new NotSupportedException();
    }
}
