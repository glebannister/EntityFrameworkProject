using EntityFrameworkProject.Model;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=localhost;Database=Product;Trusted_Connection=True;TrustServerCertificate=True;";

            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Product> Products { get; set; }
    }
}
