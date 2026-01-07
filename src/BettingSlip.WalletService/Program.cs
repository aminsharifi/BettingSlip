using BettingSlip.WalletService.Consumers;

var builder = WebApplication.CreateBuilder(args);

// ─────────────────────────────────────────────
// Kestrel: ECS listens on 5011
// ─────────────────────────────────────────────
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5011); // ECS container port
    // options.ListenAnyIP(5012); // removed HTTPS for simplicity
});

// ─────────────────────────────────────────────
// MassTransit / RabbitMQ
// ─────────────────────────────────────────────
builder.Services.AddMassTransit(x =>
{
    // Consumer registration
    x.AddConsumer<BetPlacedConsumer>(cfg =>
    {
        cfg.UseMessageRetry(r =>
        {
            r.Interval(3, TimeSpan.FromSeconds(5));
        });
    });

    // RabbitMQ configuration
    x.UsingRabbitMq((context, cfg) =>
    {
        var host = builder.Configuration["RabbitMQ:Host"] ?? "localhost";
        var user = builder.Configuration["RabbitMQ:Username"] ?? "guest";
        var pass = builder.Configuration["RabbitMQ:Password"] ?? "guest";

        cfg.Host(host, h =>
        {
            h.Username(user);
            h.Password(pass);
        });

        cfg.ReceiveEndpoint("wallet-service", e =>
        {
            e.PrefetchCount = 16;
            e.ConcurrentMessageLimit = 8;
            e.UseInMemoryInboxOutbox(context); // ensures safe message handling
            e.ConfigureConsumer<BetPlacedConsumer>(context);
        });
    });
});

// ─────────────────────────────────────────────
// Build app
// ─────────────────────────────────────────────
var app = builder.Build();

// ─────────────────────────────────────────────
// ECS health check endpoint
// ─────────────────────────────────────────────
app.MapHealthChecks("/health");

// ─────────────────────────────────────────────
// Default route for testing
// ─────────────────────────────────────────────
app.MapGet("/", () => "WalletService running...");

// ─────────────────────────────────────────────
// Run app
// ─────────────────────────────────────────────
app.Run();
