using GlobalMarket.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalMarket.Core.Configuration
{
    public class ShopEntityTypeConfiguration : IEntityTypeConfiguration<Shop>
    {
        public void Configure(EntityTypeBuilder<Shop> builder)
        {
            builder
                .HasIndex(shop => shop.Name)
                .IsUnique();
        }
    }
}
