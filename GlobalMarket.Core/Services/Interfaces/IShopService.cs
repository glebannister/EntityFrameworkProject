using GlobalMarket.Core.Models;
using GlobalMarket.Dto;

namespace GlobalMarket.Core.Services.Interfaces
{
    public interface IShopService
    {
        public Task<Shop> AddShop(ShopCreateDto shopCreateDto);

        public Task<Shop> UpdateShop(ShopUpdateDto shopUpdateDto);

        public Task<ProductShop> AddProductToShop(string productName, string shopName);

        public Task<Shop> DeleteShop(string name);

        public Task DeleteAllShops();

        public Task<List<Product>> GetProductsFromShop(string shopName);

        public Task<List<Shop>> GetShopsWithProducts(string productName);
    }
}
