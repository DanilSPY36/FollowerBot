using FollowerBot.Models;
using Microsoft.EntityFrameworkCore;

public class DrinkTTKDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<DrinkTTK> Drinks { get; set; }
    public DbSet<Shipper> Shippers {  get; set; } 
    public DbSet<Items> Items { get; set; }

}

