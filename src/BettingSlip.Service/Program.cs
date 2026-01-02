using BettingSlip.Application.BettingSlips.Services;
using BettingSlip.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

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
var app = builder.Build();

// Swagger setup
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

// Map controllers
app.MapControllers();

app.Run();
