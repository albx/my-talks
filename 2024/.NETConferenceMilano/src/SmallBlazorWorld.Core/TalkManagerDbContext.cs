using Microsoft.EntityFrameworkCore;
using SmallBlazorWorld.Core.Models;

namespace SmallBlazorWorld.Core;

public class TalkManagerDbContext : DbContext
{
    public TalkManagerDbContext(DbContextOptions<TalkManagerDbContext> options)
        : base(options)
    {
    }

    public DbSet<Event> Events { get; set; } = default!;

    public DbSet<Talk> Talks { get; set; } = default!;

    public DbSet<Proposal> Proposals { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Event>(e =>
        {
            e.HasKey(e => e.Id);
            e.Property(e => e.Id).ValueGeneratedNever();

            e.Property(e => e.Name).IsRequired().HasMaxLength(255);
            e.Property(e => e.Location).IsRequired().HasMaxLength(255);
        });

        modelBuilder.Entity<Talk>(t =>
        {
            t.HasKey(t => t.Id);
            t.Property(t => t.Id).ValueGeneratedNever();

            t.Property(t => t.Title).IsRequired().HasMaxLength(100);
            t.Property(t => t.Abstract).IsRequired().HasMaxLength(255);
        });

        modelBuilder.Entity<Proposal>(p =>
        {
            p.HasKey(p => p.Id);
            p.Property(p => p.Id).ValueGeneratedNever();

            p.Property(p => p.State).HasConversion<string>();

            p.HasOne(p => p.Event).WithMany();
            p.HasOne(p => p.Talk).WithMany();
        });
    }
}
