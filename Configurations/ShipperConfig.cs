
using FollowerBot.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ShipperConfig : IEntityTypeConfiguration<Shipper>
{
    

    public void Configure(EntityTypeBuilder<Shipper> builder)
    {
        builder.HasKey(x => x.ID);

        builder.HasMany(s => s.Items).WithOne(s => s.ShipperLink).HasForeignKey(s => s.Id);
    }
}
