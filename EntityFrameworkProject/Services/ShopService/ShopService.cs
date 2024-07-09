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

        public async Task<List<Product>> GetProductsFromShop(string shopName)
        {
            var shop = await _iShopRepository.GetShopAsync(shopName);

            if (shop is null) 
            {
                throw new NotFoundException($"Shop with name: [{shopName}] is not found in the DB");
            }

            var productsFromShop = await _iShopRepository.GetProductsAsync(shopName);

            if (!productsFromShop.Any()) 
            {
                throw new NotFoundException($"Shop with name: [{shopName}] does not have any products");
            }

            return productsFromShop;
        }

        public async Task<List<Shop>> GetShopsWithProducts(string productName)
        {
            var product = await _iShopRepository.GetProductAsync(productName);

            if (product is null)
            {
                throw new NotFoundException($"Product with name: [{productName}] is not found in the DB");
            }

            var shopsWithProduct = await _iShopRepository.GetShopsAsync(productName);

            if (!shopsWithProduct.Any())
            {
                throw new NotFoundException($"Product with name: [{productName}] does not exist in any shops yet. Our Soviet economy is working on it");
            }

            return shopsWithProduct;
        }

        public async Task<ProductShop> AddProductToShop(string productName, string shopName)
        {
            var productToAdd = await _iShopRepository.GetProductAsync(productName);

            if (productToAdd is null) 
            {
                throw new NotFoundException($"Product with name [{productName}] is not found in the DB");
            }

            var shop = await _iShopRepository.GetShopAsync(shopName);

            if (shop is null) 
            {
                throw new NotFoundException($"Shop with name [{shopName}] is not found in the DB");
            }

            if (await IsProductExistsInShop(productName, shopName)) 
            {
                throw new ConflictException($"Product with name [{productName}] exists in the shop [{shopName}] already");
            }

            var productShop = new ProductShop
            {
                ProductId = productToAdd.Id,
                Product = productToAdd,
                ShopId = shop.Id,
                Shop = shop
            };

            await _iShopRepository.AddProductShopAsync(productShop);

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
            await _iShopRepository.DeleteAllAsync();
        }

        public async Task<Shop> DeleteShop(string name)
        {
            var shopToDelete = await _iShopRepository.GetShopAsync(name);

            if (shopToDelete is null) 
            {
                throw new NotFoundException($"Shop with name [{name}] is not found in the DB");
            }

            await _iShopRepository.DeleteAsync(shopToDelete);

            return shopToDelete;
        }

        public async Task<Shop> UpdateShop(ShopApiUpdateDto shopApiUpdateDto)
        {
            var shopToUpdate = await _iShopRepository.GetShopAsync(shopApiUpdateDto.OldName);

            if (shopToUpdate is null) 
            {
                throw new NotFoundException($"Shop with name [{shopApiUpdateDto.NewName}] is not found in the DB");
            }

            var possibleExistingShop = await _iShopRepository.GetShopAsync(shopApiUpdateDto.NewName);

            if (possibleExistingShop is not null)
            {
                throw new ConflictException($"Shop with name [{shopApiUpdateDto.NewName}] exists in the DB already");
            }

            shopToUpdate.Name = shopApiUpdateDto.NewName;
            shopToUpdate.Address = shopApiUpdateDto.NewAddress;
            
            await _iShopRepository.SaveChangesAsync();

            return shopToUpdate;
        }

        private async Task<bool> IsProductExistsInShop(string productName, string shopName)
        {
            var productsInTheShop = await _iShopRepository.GetProductsAsync(shopName);

            return productsInTheShop.Any(product => product.Name.ToLower() == productName.ToLower());
        }
    }
}
