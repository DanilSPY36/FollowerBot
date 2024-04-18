using FollowerBot.Models;
using Microsoft.EntityFrameworkCore;

public class Context : DbContext
{
    public DbSet<DrinkTTK> DrinkTTKs { get; set; } = null!;
    public DbSet<Shipper> Shippers { get; set; } = null!;
    public DbSet<Items> Items { get; set; } = null!;
    public DbSet<InviteUser> InviteUsers { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = SurfCoffeeDB.db");
    }
}