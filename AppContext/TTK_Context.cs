using FollowerBot.Models;
using Microsoft.EntityFrameworkCore;

public class TTK_Context : DbContext
{
    public DbSet<DrinkTTK> DrinkTTKs { get; set; } = null!;
    public DbSet<DimCategory> DimCategories { get; set; } = null!;
    public DbSet<DimContainer> DimContainers { get; set; } = null!;
    public DbSet<DimVolume> DimVolumes { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = TTK_DB.db");
    }
}
