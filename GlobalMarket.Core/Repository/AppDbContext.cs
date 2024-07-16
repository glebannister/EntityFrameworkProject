using GlobalMarket.Core.Configuration;
using GlobalMarket.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace GlobalMarket.Core.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Manufacture> Manufactures { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<ProductShop> ProductShops { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ManufactureEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductShopEntityTypeConfiguration());
        }
    }
}
