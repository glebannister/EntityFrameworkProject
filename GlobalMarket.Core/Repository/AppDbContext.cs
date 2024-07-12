using GlobalMarket.Core.Configuration;
using GlobalMarket.Core.Models.Database;
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
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ManufactureConfiguration());
            modelBuilder.ApplyConfiguration(new ProductShopConfiguration());
        }
    }
}
