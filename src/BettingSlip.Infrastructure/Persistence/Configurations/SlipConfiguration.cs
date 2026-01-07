using BettingSlip.Core.SlipAggregate;

namespace BettingSlip.Infrastructure.Persistence.Configurations;

public class SlipConfiguration : IEntityTypeConfiguration<Slip>
{
    public void Configure(EntityTypeBuilder<Slip> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.StakeAmount)
            .HasPrecision(18, 2);

        builder.Property(e => e.PotentialWin).HasPrecision(18, 2);
        builder.Property(e => e.TotalOdds).HasPrecision(18, 4);

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .HasMaxLength(20);
        
        builder.Property(x => x.RowVersion)
            .IsRowVersion()
            .IsConcurrencyToken();

    }
}

