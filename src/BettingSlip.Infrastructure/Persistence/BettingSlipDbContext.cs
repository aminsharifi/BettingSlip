using BettingSlip.Core.SlipAggregate;
using BettingSlip.Infrastructure.Persistence.Configurations;

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
        var _name = typeof(BettingSlipDbContext).Assembly.GetName();

        modelBuilder.ApplyConfiguration(new SlipConfiguration());
        modelBuilder.ApplyConfiguration(new SelectionConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
