using EntityFrameworkProject.Entities.Model;

namespace EntityFrameworkProject.Repository.ProductRepo
{
    public interface IProductRepository
    {
        public Task AddAsync(Product product);

        public Task<Manufacture> GetManufactureAsync(int manufactureId);

        public Task<Product> GetProductAsync(string productName);

        public Task<List<Product>> GetProductsAsync(string manufactureName);

        public Task SaveChangesAsync();

        public Task DeleteAsync(Product product);

        public Task DeleteAllAsync();
    }
}
