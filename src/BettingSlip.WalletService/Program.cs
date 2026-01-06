using MassTransit;
using BettingSlip.WalletService.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5011);
    options.ListenAnyIP(5012);
});

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<BetPlacedConsumer>(cfg =>
    {
        cfg.UseMessageRetry(r =>
        {
            r.Interval(3, TimeSpan.FromSeconds(5));
        });
    });


    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQ:Host"], h =>
        {
            h.Username(builder.Configuration["RabbitMQ:Username"]);
            h.Password(builder.Configuration["RabbitMQ:Password"]);
        });
        cfg.ReceiveEndpoint("wallet-service", e =>
        {
            e.PrefetchCount = 16; 
            e.ConcurrentMessageLimit = 8;
            e.UseInMemoryInboxOutbox(context);
            e.ConfigureConsumer<BetPlacedConsumer>(context);
        });
    });
});

var app = builder.Build();
app.MapGet("/", () => "WalletService running...");
app.Run();
