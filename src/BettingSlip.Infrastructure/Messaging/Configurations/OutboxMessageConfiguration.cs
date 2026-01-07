namespace BettingSlip.Infrastructure.Messaging.Configurations;

public class OutboxMessageConfiguration :
    IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> entity)
    {
        entity.HasKey(x => x.SequenceNumber);

        entity.Property(x => x.EnqueueTime);
        entity.Property(x => x.ContentType);
        entity.Property(x => x.MessageType);
        entity.Property(x => x.Body);
        
    }
}