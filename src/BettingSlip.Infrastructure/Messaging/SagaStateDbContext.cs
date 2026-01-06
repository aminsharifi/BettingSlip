using BettingSlip.Infrastructure.Persistence.Configurations;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

public class SagaStateDbContext : SagaDbContext
{
    public SagaStateDbContext(DbContextOptions<SagaStateDbContext> options) : base(options)
    {

    }

    protected override IEnumerable<ISagaClassMap> Configurations
    {
        get { yield return new BetSagaStateConfiguration(); }
    }
}