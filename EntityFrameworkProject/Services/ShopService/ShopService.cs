using EntityFrameworkProject.Entities.Dto;
using EntityFrameworkProject.Entities.Model;
using EntityFrameworkProject.Exceptions;
using EntityFrameworkProject.Repository.ShopRepo;

namespace EntityFrameworkProject.Services.ShopService
{
    public class ShopService : IShopService
    {
        private readonly IShopRepository _iShopRepository;

        public ShopService(IShopRepository iShopRepository)
        {
            _iShopRepository = iShopRepository;
        }

        public async Task<ProductShop> AddProductToShop(string productName, string shopName)
        {
            var productToAdd = await _iShopRepository.GetProductAsync(productName);

            if (productToAdd is null) 
            {
                throw new NotFoundException($"Product with name {productName} is not found in the DB");
            }

            var shop = await _iShopRepository.GetShopAsync(shopName);

            if (shop is null) 
            {
                throw new NotFoundException($"Shop with name {shopName} is not found in the DB");
            }

            var productShop = new ProductShop
            {
                ProductId = productToAdd.Id,
                Product = productToAdd,
                ShopId = shop.Id,
                Shop = shop
            };

            await _iShopRepository.AddProductToShopAsync(productShop);

            return productShop;
        }

        public async Task<Shop> AddShop(ShopApiDto shopApiDto)
        {
            var shopToAdd = new Shop
            {
                Name = shopApiDto.Name,
                Address = shopApiDto.Address
            };

            await _iShopRepository.AddAsync(shopToAdd);

            return shopToAdd;
        }

        public async Task DeleteAllShops()
        {
            throw new NotImplementedException();
        }

        public async Task<Shop> DeleteShop(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Product>> GetProductsFromShop(string shopName)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Product>> GetShopsWithProducts(string productName)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsProductExistsInShop(string productName, string shopName)
        {
            throw new NotImplementedException();
        }

        public async Task<Shop> UpdateShop(ShopApiDto shopApiDto)
        {
            throw new NotImplementedException();
        }
    }
}
