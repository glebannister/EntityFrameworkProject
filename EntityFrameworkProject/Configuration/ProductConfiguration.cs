using EntityFrameworkProject.Entities.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkProject.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .HasOne(product => product.Manufacture)
                .WithMany()
                .HasForeignKey(product => product.ManufactureId)
                .IsRequired();

            //builder.Ignore(product => product.ManufactureId);
        }
    }
}
