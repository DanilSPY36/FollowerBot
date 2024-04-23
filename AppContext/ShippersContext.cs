using FollowerBot.Models;
using Microsoft.EntityFrameworkCore;

public class ShippersContext : DbContext
{
    public DbSet<Shipper> Shippers { get; set; } = null!;
    public DbSet<Items> Items { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ShippersDB.db");
    }
}
