using EntityFrameworkProject.Data;
using EntityFrameworkProject.Entities.Model;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkProject.Repository.ShopRepo
{
    public class ShopRepository : IShopRepository
    {
        private readonly AppDbContext _appDbContext;

        public ShopRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(Shop shop)
        {
            _appDbContext.Shops.Add(shop);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task AddProductShopAsync(ProductShop productShop)
        {
            _appDbContext.ProductShops.Add(productShop);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Shop> GetShopAsync(string shopName)
        {
            return await _appDbContext.Shops
                .FirstOrDefaultAsync(shop => shop.Name.ToLower() == shopName.ToLower());
        }

        public async Task<Product> GetProductAsync(string productName)
        {
            return await _appDbContext.Products
                .Include(product => product.Manufacture)
                .FirstOrDefaultAsync(product => product.Name.ToLower() == productName.ToLower());
        }

        public async Task<List<Product>> GetProductsAsync(string shopName)
        {
            return await _appDbContext.ProductShops
                .Where(productShop => productShop.Shop.Name.ToLower() == shopName)
                .Include(product => product.Product.Manufacture)
                .Select(productShop => productShop.Product)
                .ToListAsync();
        }

        public async Task<List<Shop>> GetShopsAsync(string productName)
        {
            return await _appDbContext.ProductShops
                .Where(productShop => productShop.Product.Name.Contains(productName))
                .Select(productShop => productShop.Shop)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAllAsync()
        {
            await _appDbContext.Shops.ForEachAsync(shop =>
            {
                _appDbContext.Shops.Remove(shop);
            });
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Shop shop)
        {
            _appDbContext.Shops.Remove(shop);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
