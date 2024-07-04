using EntityFrameworkProject.Entities.Dto;
using EntityFrameworkProject.Entities.Model;

namespace EntityFrameworkProject.Services.ShopService
{
    public interface IShopService
    {
        public Task<Shop> AddShop(ShopApiDto shopApiDto);

        public Task<Shop> UpdateShop(ShopApiDto shopApiDto);

        public Task<ProductShop> AddProductToShop(string productName, string shopName);

        public Task<Shop> DeleteShop(string name);

        public Task DeleteAllShops();

        public Task<List<Product>> GetProductsFromShop(string shopName);

        public Task<List<Product>> GetShopsWithProducts(string productName);

        public Task<bool> IsProductExistsInShop(string productName, string shopName);
    }
}
