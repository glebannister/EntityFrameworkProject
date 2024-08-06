using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalMarket.Core.Models.Configurations
{
    public class ProductShopEntityTypeConfiguration : IEntityTypeConfiguration<ProductShop>
    {
        public void Configure(EntityTypeBuilder<ProductShop> builder)
        {
            builder
                .HasKey(productShop => new { productShop.ProductId, productShop.ShopId });

            builder
                .HasOne(productShop => productShop.Product)
                .WithMany()
                .HasForeignKey(productShop => productShop.ProductId);

            builder
                .HasOne(productShop => productShop.Shop)
                .WithMany()
                .HasForeignKey(productShop => productShop.ShopId);
        }
    }
}
