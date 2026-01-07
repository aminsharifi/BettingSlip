using BettingSlip.Application.BettingSlips.Messaging;
using BettingSlip.Contracts.Events;


namespace BettingSlip.Infrastructure.Messaging;

public static class RabbitMqBusConfigurator
{
    public static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.AddSagaStateMachine<BetSaga, BetSagaState>()
              .EntityFrameworkRepository(r =>
              {
                  r.ConcurrencyMode = ConcurrencyMode.Optimistic;
                  r.AddDbContext<DbContext, SagaStateDbContext>((provider, builder) =>
                  {
                      builder.UseSqlServer(configuration.GetConnectionString("SagaStateConnection"), m =>
                      {
                          m.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                          m.MigrationsHistoryTable($"__{nameof(SagaStateDbContext)}");
                      });
                  });
              });

            x.AddEntityFrameworkOutbox<SagaStateDbContext>(o =>
            {
                o.QueryDelay = TimeSpan.FromSeconds(1);
                o.UseSqlServer();
                o.UseBusOutbox();
            });

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration["RabbitMQ:Host"], h =>
                {
                    h.Username(configuration["RabbitMQ:Username"]);
                    h.Password(configuration["RabbitMQ:Password"]);
                });
                cfg.Publish<BetPlaced>(p =>
                {
                    p.Durable = true;
                });
            });
        });
        
        services.AddScoped<IBetPublisher, BetPublisher>();
    }
}
