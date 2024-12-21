using Microsoft.EntityFrameworkCore;
using SmallBlazorWorld.Core.Models;

namespace SmallBlazorWorld.Core.Services;

public class TalkCommands(TalkManagerDbContext context)
{
    public TalkManagerDbContext Context { get; } = context;

    public async Task CreateNewTalkAsync(string title, string @abstract)
    {
        var talk = new Talk
        {
            Id = Guid.NewGuid(),
            Title = title,
            Abstract = @abstract,
        };

        Context.Talks.Add(talk);
        await Context.SaveChangesAsync();
    }

    public async Task ProposeTalkAsync(Guid talkId, Guid eventId)
    {
        var talk = await Context.Talks.FindAsync(talkId);
        var @event = await Context.Events.FindAsync(eventId);

        var proposal = new Proposal
        {
            Id = Guid.NewGuid(),
            Talk = talk!,
            Event = @event!,
            State = Proposal.ProposalState.PendingApproval
        };

        Context.Proposals.Add(proposal);
        await Context.SaveChangesAsync();
    }

    public async Task ApproveTalkAsync(Guid talkId, Guid eventId)
    {
        var proposal = Context.Proposals
            .Include(p => p.Talk)
            .Include(p => p.Event)
            .Where(p => p.Talk.Id == talkId && p.Event.Id == eventId)
            .SingleOrDefault();

        if (proposal is null) 
        {
            throw new InvalidOperationException("Proposal does not exist");
        }

        proposal.State = Proposal.ProposalState.Approved;
        await Context.SaveChangesAsync();
    }
}
