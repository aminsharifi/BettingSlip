using BettingSlip.Application.BettingSlips.Messaging;
using BettingSlip.Application.BettingSlips.Services;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BettingSlip.Infrastructure.Messaging;

public static class RabbitMqBusConfigurator
{
    public static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration["RabbitMQ:Host"], h =>
                {
                    h.Username(configuration["RabbitMQ:Username"]);
                    h.Password(configuration["RabbitMQ:Password"]);
                });
            });
        });
        
        services.AddScoped<IBetPublisher, BetPublisher>();
    }
}
