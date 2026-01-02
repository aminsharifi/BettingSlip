namespace BettingSlip.Infrastructure.Persistence.Configurations;

using BettingSlip.Core.SlipAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
               .HasColumnType("decimal(10,2)")
               .IsRequired();
    }
}