using FollowerBot.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
public class DrinkTTKConfig : IEntityTypeConfiguration<DrinkTTK>
{
    public void Configure(EntityTypeBuilder<DrinkTTK> builder)
    {
        builder.HasKey(a => a.ID);
    }
}
