using BettingSlip.Application.BettingSlips.Services;
using BettingSlip.Infrastructure.Messaging;
using BettingSlip.Infrastructure.Observability;
using BettingSlip.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// ─────────────────────────────────────────────
// Kestrel config
// ─────────────────────────────────────────────
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5001); // ECS/Fargate port
    //options.ListenAnyIP(5002); // optional HTTPS for local only
});

// ─────────────────────────────────────────────
// Services
// ─────────────────────────────────────────────
// Register MassTransit via Infrastructure layer
builder.Services.AddRabbitMq(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ─────────────────────────────────────────────
// CORS (Allow all for now)
// ─────────────────────────────────────────────
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// ─────────────────────────────────────────────
// Application services
// ─────────────────────────────────────────────
builder.Services.AddScoped<BettingSlipService>();
builder.Services.AddInfrastructure(builder.Configuration);

// ─────────────────────────────────────────────
// OpenTelemetry
// ─────────────────────────────────────────────
builder.Services.AddOpenTelemetry()
    .WithTracing(tracerBuilder =>
    {
        tracerBuilder
            .AddSource(TracingSources.MassTransit)
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddEntityFrameworkCoreInstrumentation()
            .AddOtlpExporter(); // optional
    });

// ─────────────────────────────────────────────
// App build
// ─────────────────────────────────────────────
var app = builder.Build();



static void ApplyMigrations<TContext>(WebApplication app)
where TContext : DbContext
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<TContext>();
    db.Database.Migrate();
}

ApplyMigrations<BettingSlipDbContext>(app);

ApplyMigrations<SagaStateDbContext>(app);

// ─────────────────────────────────────────────
// Middleware
// ─────────────────────────────────────────────
app.UseSwagger();
app.UseSwaggerUI();


// Only redirect to HTTPS in Development
if (builder.Environment.IsDevelopment())
{
    //app.UseHttpsRedirection();
}

app.UseAuthorization();
app.UseCors("AllowAll");

// ─────────────────────────────────────────────
// Map endpoints
// ─────────────────────────────────────────────
app.MapControllers();
app.MapHealthChecks("/health"); // ECS health check

app.Run();
