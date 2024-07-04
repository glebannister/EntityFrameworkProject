using EntityFrameworkProject.Entities.Model;

namespace EntityFrameworkProject.Repository.ShopRepo
{
    public interface IShopRepository
    {
        public Task AddAsync(Shop shop);

        public Task AddProductToShopAsync(ProductShop productShop);

        public Task<List<Shop>> GetShopsAsync(string productName);

        public Task<List<Product>> GetProductsAsync(string shopName);

        public Task<Shop> GetShopAsync(string shopName);

        public Task<Product> GetProductAsync(string productName);

        public Task SaveChangesAsync();

        public Task DeleteAsync(Shop shop);

        public Task DeleteAllAsync();
    }
}
