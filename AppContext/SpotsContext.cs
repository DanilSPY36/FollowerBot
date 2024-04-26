using Microsoft.EntityFrameworkCore;

public class SpotsContext : DbContext
{
    public DbSet<Spot> SpotsInfo { get; set; } = null!;
    public DbSet<Position> Positions { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = SpotsDB.db");
    }

}