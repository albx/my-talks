using Microsoft.EntityFrameworkCore;

namespace BlazorUIWars.Core;

public class ExpenseReportDbContext : DbContext
{
    public ExpenseReportDbContext(DbContextOptions<ExpenseReportDbContext> options)
        : base(options)
    {
    }

    public DbSet<ExpenseReport> ExpenseReports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure the ExpenseReport entity
        modelBuilder.Entity<ExpenseReport>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Notes).HasMaxLength(1000);
            entity.Property(e => e.Date).IsRequired();
            entity.Property(e => e.Amount).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(e => e.Location).HasMaxLength(200);
            entity.Property(e => e.Type).IsRequired().HasConversion<string>();
        });
    }
}
