using BettingSlip.Infrastructure.Messaging;
using BettingSlip.Infrastructure.Messaging.Configurations;

public class SagaStateDbContext : SagaDbContext
{
    public SagaStateDbContext(DbContextOptions<SagaStateDbContext> options) : base(options)
    {

    }

    public DbSet<OutboxState> OutboxStates { get; set; }
    public DbSet<BetSagaState> BetSagaStates { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }


    protected override IEnumerable<ISagaClassMap> Configurations
    {
        get { yield return new BetSagaStateConfiguration(); }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.ApplyConfiguration(new BetSagaStateConfiguration());
        modelBuilder.ApplyConfiguration(new OutboxStateConfiguration());
        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}