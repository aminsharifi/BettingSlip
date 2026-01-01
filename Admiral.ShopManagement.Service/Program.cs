using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();            // <--- must have this
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure SQL Server DbContext (Windows Authentication)
builder.Services.AddDbContext<AppDbContext>(options =>
    // options.UseSqlServer("Server=localhost;Database=AdmiralShopDb;Trusted_Connection=True;TrustServerCertificate=True;")
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);    
var app = builder.Build();

// Swagger setup
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

// Map controllers
app.MapControllers();

app.Run();
