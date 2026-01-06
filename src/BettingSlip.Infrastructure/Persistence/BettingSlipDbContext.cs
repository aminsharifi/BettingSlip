using BettingSlip.Core.SlipAggregate;
using Microsoft.EntityFrameworkCore;

namespace BettingSlip.Infrastructure.Persistence;

public class BettingSlipDbContext(DbContextOptions<BettingSlipDbContext> options) : DbContext(options)
{    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    public DbSet<Slip> Slips => Set<Slip>();
    public DbSet<Selection> Selections => Set<Selection>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BettingSlipDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
