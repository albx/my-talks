using SmallBlazorWorld.App.Shared.Models;
using SmallBlazorWorld.App.Shared.Services;

namespace SmallBlazorWorld.App.Services;

public class EventsService : IEventsService
{
    public async Task<EventModel[]> GetEventsAsync()
    {
        await Task.Delay(200);
        return InMemoryEventsRepository.Events;
    }

    public async Task<MyEventModel[]> GetMyEventsAsync()
    {
        await Task.Delay(200);
        return InMemoryEventsRepository.MyEvents.ToArray();
    }

    public async Task<TalkModel[]> GetTalksAsync()
    {
        await Task.Delay(200);
        return InMemoryEventsRepository.Talks;
    }

    public Task SubmitProposalAsync(SubmitProposalModel model)
    {
        var talk = InMemoryEventsRepository.Talks.Single(t => t.Id == model.TalkId);
        var @event = InMemoryEventsRepository.Events.Single(e => e.Id == model.EventId);

        var proposal = new MyEventModel
        {
            EventDate = @event.Date,
            EventLocation = @event.Location,
            EventName = @event.Name,
            TalkTitle = talk.Title
        };

        InMemoryEventsRepository.MyEvents.Add(proposal);

        return Task.CompletedTask;
    }

    internal static class InMemoryEventsRepository
    {
        public static List<MyEventModel> MyEvents { get; } = [];

        public static EventModel[] Events { get; } = [
            new() { Id = Guid.Parse("8647a740-81f5-4029-9d82-396a1051dff2"), Name = "An online event about .NET", Date = new(2025, 2, 13), Location = "Online" },
            new() { Id = Guid.Parse("1ca8f1a7-5d96-4b57-a879-5e541abb79d2"), Name = "Web day 2025", Date = new(2025, 3, 31), Location = "TAG Calabiana, Milano" },
            new() { Id = Guid.Parse("75eff421-2e16-43ff-8f56-b754ad991074"), Name = ".NET Conference Milano 2024", Date = new(2024, 12, 16), Location = "Microsoft House, Milano" },
        ];

        public static TalkModel[] Talks { get; } = [
            new() { Id = Guid.Parse("5a59a541-189e-4935-b31a-1b11c46e2055"), Title = "Blazor tutti i gusti più uno", Abstract = "In questa sessione vedremo insieme come Blazor possa essere utilizzato al di fuori del mondo Web" },
            new() { Id = Guid.Parse("b8854764-f534-49c3-adb8-c42df1d8789a"), Title = "Da SSR a WebAssembly: ecco Blazor 8", Abstract = "Con .NET 8 Blazor si è evoluto in modo da essere uno strumento completo per le nostre applicazioni web. Vediamo insieme quali sono le funzionalità chiave" },
        ];
    }
}
