using EntityFrameworkProject.Data;
using EntityFrameworkProject.Entities.Model;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkProject.Repository.ProductRepo
{
    public class ProductsRepository : IProductRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProductsRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(Product product)
        {
            _appDbContext.Products.Add(product);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Manufacture> GetManufactureAsync(int manufactureId)
        {
            return await _appDbContext.Manufactures
                .FirstOrDefaultAsync(manufactue => manufactue.Id == manufactureId);
        }

        public async Task<Product> GetProductAsync(string productName)
        {
            return await _appDbContext.Products
                .FirstOrDefaultAsync(product => product.Name.ToLower() == productName.ToLower());
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _appDbContext.Products.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            _appDbContext.Products.Remove(product);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
