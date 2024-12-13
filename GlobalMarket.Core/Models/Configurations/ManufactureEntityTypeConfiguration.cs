using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalMarket.Core.Models.Configurations
{
    public class ManufactureEntityTypeConfiguration : IEntityTypeConfiguration<Manufacture>
    {
        public void Configure(EntityTypeBuilder<Manufacture> builder)
        {
            builder
                .HasIndex(manufacture => manufacture.Name)
                .IsUnique();
        }
    }
}
