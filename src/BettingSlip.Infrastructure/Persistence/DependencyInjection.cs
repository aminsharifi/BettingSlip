using BettingSlip.Application.Abstractions.Persistence;
using BettingSlip.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BettingSlip.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<BettingSlipDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IBettingSlipRepository, BettingSlipRepository>();

        services.AddScoped<IUnitOfWork, EfUnitOfWork>();


        return services;
    }
}
