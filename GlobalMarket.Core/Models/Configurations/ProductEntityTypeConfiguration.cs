using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalMarket.Core.Models.Configurations
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .HasOne(product => product.Manufacture)
                .WithMany()
                .HasForeignKey(product => product.ManufactureId)
                .IsRequired();

            builder
                .HasIndex(product => product.Name)
                .IsUnique();
        }
    }
}
