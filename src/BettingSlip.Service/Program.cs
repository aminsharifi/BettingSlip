using BettingSlip.Application.BettingSlips.Services;
using BettingSlip.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();            // <--- must have this
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure SQL Server DbContext (Windows Authentication)
builder.Services.AddDbContext<BettingSlipDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
// Application services
builder.Services.AddScoped<BettingSlipService>(); 
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

static void ApplyMigrations(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<BettingSlipDbContext>();

    for (int i = 0; i < 10; i++)
    {
        try
        {
            db.Database.Migrate();
            return;
        }
        catch
        {
            Thread.Sleep(3000);
        }
    }

    throw new Exception("Database migration failed after retries.");
}


var app = builder.Build();

ApplyMigrations(app);

// Swagger setup
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseCors("AllowAll");


// Map controllers
app.MapControllers();


app.Run();
