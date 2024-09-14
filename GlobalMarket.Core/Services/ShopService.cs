using GlobalMarket.Core.Dto;
using GlobalMarket.Core.Exceptions;
using GlobalMarket.Core.Models;
using GlobalMarket.Core.Repository;
using GlobalMarket.Core.Services.Interfaces;
using GlobalMarket.Dto;
using Microsoft.EntityFrameworkCore;

namespace GlobalMarket.Core.Services
{
    public class ShopService : IShopService
    {
        private readonly AppDbContext _appDbContext;

        public ShopService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<GetProductDto>> GetProductsFromShop(string shopName)
        {
            var shop = await GetShopDbAsync(shopName);

            if (shop is null)
            {
                throw new NotFoundException($"Shop with name: [{shopName}] is not found in the DB");
            }

            var productsFromShop = await GetProductsDbAsync(shopName);
            
            if (!productsFromShop.Any())
            {
                throw new NotFoundException($"Shop with name: [{shopName}] does not have any products");
            }

            var resultProductsList = productsFromShop.Select(product => new GetProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Manufacture = product.Manufacture
            }).ToList();

            return resultProductsList;
        }

        public async Task<List<Shop>> GetShopsWithProducts(string productName)
        {
            var product = await GetProductDbAsync(productName);

            if (product is null)
            {
                throw new NotFoundException($"Product with name: [{productName}] is not found in the DB");
            }

            var shopsWithProduct = await _appDbContext.ProductShops
                .Where(productShop => productShop.Product.Name.Contains(productName))
                .Select(productShop => productShop.Shop)
                .ToListAsync();

            if (!shopsWithProduct.Any())
            {
                throw new NotFoundException($"Product with name: [{productName}] does not exist in any shops yet. Our Soviet economy is working on it");
            }

            return shopsWithProduct;
        }

        public async Task<ProductShop> AddProductToShop(string productName, string shopName)
        {
            var productToAdd = await GetProductDbAsync(productName);

            if (productToAdd is null)
            {
                throw new NotFoundException($"Product with name [{productName}] is not found in the DB");
            }

            var shop = await GetShopDbAsync(shopName);

            if (shop is null)
            {
                throw new NotFoundException($"Shop with name [{shopName}] is not found in the DB");
            }

            if (await IsProductExistsInShop(productName, shopName))
            {
                throw new AlreadyExistException($"Product with name [{productName}] exists in the shop [{shopName}] already");
            }

            var productShop = new ProductShop
            {
                ProductId = productToAdd.Id,
                Product = productToAdd,
                ShopId = shop.Id,
                Shop = shop
            };

            await AddProductShopAsync(productShop);

            return productShop;
        }

        public async Task<Shop> AddShop(ShopCreateDto shopCreateDto)
        {
            var shopToAdd = new Shop
            {
                Name = shopCreateDto.Name,
                Address = shopCreateDto.Address
            };

            await AddAsync(shopToAdd);

            return shopToAdd;
        }

        public async Task DeleteAllShops()
        {
            await _appDbContext.Shops.ForEachAsync(shop =>
            {
                _appDbContext.Shops.Remove(shop);
            });

            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Shop> DeleteShop(string name)
        {
            var shopToDelete = await GetShopDbAsync(name);

            if (shopToDelete is null)
            {
                throw new NotFoundException($"Shop with name [{name}] is not found in the DB");
            }

            _appDbContext.Shops.Remove(shopToDelete);
            await _appDbContext.SaveChangesAsync();

            return shopToDelete;
        }

        public async Task<Shop> UpdateShop(ShopUpdateDto shopUpdateDto)
        {
            var shopToUpdate = await GetShopDbAsync(shopUpdateDto.OldName);

            if (shopToUpdate is null)
            {
                throw new NotFoundException($"Shop with name [{shopUpdateDto.NewName}] is not found in the DB");
            }

            var possibleExistingShop = await GetShopDbAsync(shopUpdateDto.NewName);

            if (possibleExistingShop is not null)
            {
                throw new AlreadyExistException($"Shop with name [{shopUpdateDto.NewName}] exists in the DB already");
            }

            shopToUpdate.Name = shopUpdateDto.NewName;
            shopToUpdate.Address = shopUpdateDto.NewAddress;

            await _appDbContext.SaveChangesAsync();

            return shopToUpdate;
        }

        private async Task<bool> IsProductExistsInShop(string productName, string shopName)
        {
            var productsInTheShop = await GetProductsDbAsync(shopName);

            return productsInTheShop.Any(product => product.Name.ToLower() == productName.ToLower());
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

        private async Task<Shop> GetShopDbAsync(string shopName)
        {
            return await _appDbContext.Shops
                .FirstOrDefaultAsync(shop => shop.Name.ToLower() == shopName.ToLower());
        }

        private async Task<Product> GetProductDbAsync(string productName)
        {
            return await _appDbContext.Products
                .Include(product => product.Manufacture)
                .FirstOrDefaultAsync(product => product.Name.ToLower() == productName.ToLower());
        }

        private async Task<List<Product>> GetProductsDbAsync(string shopName)
        {
            return await _appDbContext.ProductShops
                .Where(productShop => productShop.Shop.Name.ToLower() == shopName)
                .Include(product => product.Product.Manufacture)
                .Select(productShop => productShop.Product)
                .ToListAsync();
        }
    }
}
