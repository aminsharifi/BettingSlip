using BettingSlip.Application.Abstractions.Persistence;
using BettingSlip.Infrastructure.Persistence.Repositories;

namespace BettingSlip.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<BettingSlipDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddDbContext<SagaStateDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("SagaStateConnection")));

        services.AddScoped<IBettingSlipRepository, BettingSlipRepository>();

        services.AddScoped<IUnitOfWork, EfUnitOfWork>();

        return services;
    }
}