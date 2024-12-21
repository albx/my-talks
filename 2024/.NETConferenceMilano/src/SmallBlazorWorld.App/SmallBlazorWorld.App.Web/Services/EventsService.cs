using Microsoft.EntityFrameworkCore;
using SmallBlazorWorld.App.Shared.Models;
using SmallBlazorWorld.App.Shared.Services;
using SmallBlazorWorld.Core.Services;

namespace SmallBlazorWorld.App.Web.Services;

public class EventsService(Database database, TalkCommands commands) : IEventsService
{
    public Database Database { get; } = database;
    public TalkCommands Commands { get; } = commands;

    public async Task<EventModel[]> GetEventsAsync()
    {
        var events = await Database.Events
            .OrderBy(e => e.Date)
            .Select(e => new EventModel
            {
                Id = e.Id,
                Date = e.Date,
                Location = e.Location,
                Name = e.Name
            }).ToArrayAsync();

        return events;
    }

    public async Task<MyEventModel[]> GetMyEventsAsync()
    {
        var myEvents = await Database.Proposals
            .OrderBy(p => p.Event.Date)
            .Select(p => new MyEventModel
            {
                EventDate = p.Event.Date,
                EventLocation = p.Event.Location,
                EventName = p.Event.Name,
                TalkTitle = p.Talk.Title
            }).ToArrayAsync();

        return myEvents;
    }

    public async Task<TalkModel[]> GetTalksAsync()
    {
        var talks = await Database.Talks
            .OrderBy(t => t.Title)
            .Select(t => new TalkModel
            {
                Id = t.Id,
                Title = t.Title,
                Abstract = t.Abstract
            }).ToArrayAsync();

        return talks;
    }

    public async Task SubmitProposalAsync(SubmitProposalModel model)
    {
        await Commands.ProposeTalkAsync(model.TalkId, model.EventId);
    }
}
