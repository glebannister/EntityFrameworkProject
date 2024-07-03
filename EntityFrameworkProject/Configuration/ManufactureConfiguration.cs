using EntityFrameworkProject.Entities.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkProject.Configuration
{
    public class ManufactureConfiguration : IEntityTypeConfiguration<Manufacture>
    {
        public void Configure(EntityTypeBuilder<Manufacture> builder)
        {
            builder
                .HasIndex(manufacture => manufacture.Name)
                .IsUnique();
        }
    }
}
