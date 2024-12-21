using SmallBlazorWorld.App.Shared.Models;

namespace SmallBlazorWorld.App.Shared.Services;

public interface IEventsService
{
    Task<MyEventModel[]> GetMyEventsAsync();

    Task<EventModel[]> GetEventsAsync();

    Task<TalkModel[]> GetTalksAsync();

    Task SubmitProposalAsync(SubmitProposalModel model);
}
