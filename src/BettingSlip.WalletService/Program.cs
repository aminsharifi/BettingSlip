using MassTransit;
using BettingSlip.WalletService.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<BetPlacedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("wallet-service", e =>
        {
            e.ConfigureConsumer<BetPlacedConsumer>(context);
        });
    });
});

var app = builder.Build();
app.MapGet("/", () => "WalletService running...");
app.Run();
