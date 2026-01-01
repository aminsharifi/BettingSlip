using BettingSlip.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Admiral.ShopManagement.Service.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    { }

    public DbSet<Shop> Shops => Set<Shop>();
    public DbSet<AppUser> Users => Set<AppUser>();
}
