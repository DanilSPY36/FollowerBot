using FollowerBot.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ItemsCofig : IEntityTypeConfiguration<Items>
{
    public void Configure(EntityTypeBuilder<Items> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(s => s.ShipperLink).WithMany(i => i.Items);
    }
}
