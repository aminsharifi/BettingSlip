namespace BettingSlip.Infrastructure.Messaging.Configurations;

public class BetSagaStateConfiguration : SagaClassMap<BetSagaState>
{
    protected override void Configure(
        EntityTypeBuilder<BetSagaState> entity,
        ModelBuilder model)
    {
        entity.Property(x => x.CurrentState)
              .HasMaxLength(64);

        entity.Property(x => x.Amount)
              .HasPrecision(18, 4); // or 18,2 depending on business rules

        entity.Property(x => x.RowVersion)
              .IsRowVersion();
    }
}
