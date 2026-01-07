namespace BettingSlip.Infrastructure.Persistence.Configurations;

using BettingSlip.Core.SlipAggregate;


public class SelectionConfiguration : IEntityTypeConfiguration<Selection>
{
    public void Configure(EntityTypeBuilder<Selection> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.EventName)
               .HasMaxLength(200)
               .IsRequired();

        builder.Property(x => x.Market)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(x => x.Odd)
               .HasPrecision(18, 2)
               .IsRequired();
    }
}