namespace BettingSlip.Infrastructure.Messaging.Configurations;

public class OutboxStateConfiguration :
    IEntityTypeConfiguration<OutboxState>
{
    public void Configure(EntityTypeBuilder<OutboxState> entity)
    {
        entity.HasKey(e => e.OutboxId);
        entity.Property(e => e.RowVersion)
              .IsRowVersion();                    
        entity.Property(e => e.Created)
              .IsRequired();
    }
}
