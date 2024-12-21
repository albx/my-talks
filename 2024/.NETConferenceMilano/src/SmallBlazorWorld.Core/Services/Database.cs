using Microsoft.EntityFrameworkCore;
using SmallBlazorWorld.Core.Models;

namespace SmallBlazorWorld.Core.Services;

public class Database(TalkManagerDbContext context)
{
    public TalkManagerDbContext Context { get; } = context;

    public IQueryable<Talk> Talks => Context.Talks.AsNoTracking();

    public IQueryable<Event> Events => Context.Events.AsNoTracking();

    public IQueryable<Proposal> Proposals => Context.Proposals.Include(p => p.Talk).Include(p => p.Event).AsNoTracking();
}
