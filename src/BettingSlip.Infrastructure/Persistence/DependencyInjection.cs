using BettingSlip.Application.Abstractions.Persistence;
using BettingSlip.Infrastructure.Persistence.Repositories;

namespace BettingSlip.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Use environment variables first, fallback to appsettings
        var defaultConnection = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION")
                                ?? configuration.GetConnectionString("DefaultConnection");

        var sagaConnection = Environment.GetEnvironmentVariable("SAGA_CONNECTION")
                             ?? configuration.GetConnectionString("SagaStateConnection");

        services.AddDbContext<BettingSlipDbContext>(options =>
            options.UseSqlServer(defaultConnection));

        services.AddDbContext<SagaStateDbContext>(options =>
            options.UseSqlServer(sagaConnection));

        services.AddScoped<IBettingSlipRepository, BettingSlipRepository>();
        services.AddScoped<IUnitOfWork, EfUnitOfWork>();

        return services;
    }
}
