using GlobalMarket.Core.Models.Api;
using GlobalMarket.Core.Models.Database;

namespace GlobalMarket.Core.Services.ShopService
{
    public interface IShopService
    {
        public Task<Shop> AddShop(ShopApi shopApiDto);

        public Task<Shop> UpdateShop(ShopUpdateApi shopApiDto);

        public Task<ProductShop> AddProductToShop(string productName, string shopName);

        public Task<Shop> DeleteShop(string name);

        public Task DeleteAllShops();

        public Task<List<Product>> GetProductsFromShop(string shopName);

        public Task<List<Shop>> GetShopsWithProducts(string productName);
    }
}
