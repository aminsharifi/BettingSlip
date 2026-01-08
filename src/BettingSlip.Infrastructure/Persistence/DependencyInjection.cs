using BettingSlip.Application.Abstractions.Persistence;
using BettingSlip.Infrastructure.Persistence.Repositories;

namespace BettingSlip.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        // Use environment variables if present, otherwise fallback to config
        var defaultConnection = configuration.GetConnectionString("DefaultConnection");

        var sagaConnection = configuration.GetConnectionString("SagaStateConnection");

        services.AddDbContext<BettingSlipDbContext>(options =>
            options.UseSqlServer(defaultConnection));

        services.AddDbContext<SagaStateDbContext>(options =>
            options.UseSqlServer(sagaConnection));

        services.AddScoped<IBettingSlipRepository, BettingSlipRepository>();
        services.AddScoped<IUnitOfWork, EfUnitOfWork>();

        return services;
    }
}
