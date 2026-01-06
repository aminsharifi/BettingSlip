using BettingSlip.Infrastructure.Messaging;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BettingSlip.Infrastructure.Persistence.Configurations;

public class BetSagaStateConfiguration :
    SagaClassMap<BetSagaState>
{
    protected override void Configure(EntityTypeBuilder<BetSagaState> entity, ModelBuilder model)
    {
        entity.Property(x => x.CurrentState).HasMaxLength(64);
        entity.Property(x => x.Amount);
        entity.Property(x => x.RowVersion).IsRowVersion();
    }
}
