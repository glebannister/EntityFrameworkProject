using GlobalMarket.Core.Exceptions;
using GlobalMarket.Core.Models.Api;
using GlobalMarket.Core.Models.Database;
using GlobalMarket.Core.Repository;
using GlobalMarket.Core.Services.ProductSerivce;
using Microsoft.EntityFrameworkCore;

namespace GlobalMarket.Core.Services.ProductService
{
    public class ProductsService : IProductsService
    {
        private readonly AppDbContext _appDbContext;

        public ProductsService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Product> AddProduct(ProductApi productApiDto)
        {
            var productToAdd = await GetProductDbAsync(productApiDto.Name);

            if (productToAdd is null)
            {
                throw new ConflictException($"Prodcut with the name [{productApiDto.Name}] exists in the DB already");
            }

            var manufacture = await GetManufactureDbAsync(productApiDto.ManufactureId);

            if (manufacture is null)
            {
                throw new NotFoundException($"No manufactues with ID [{productApiDto.ManufactureId}] have been found");
            }

            Product newProduct = new Product
            {
                Name = productApiDto.Name,
                Description = productApiDto.Description,
                Price = productApiDto.Price,
                ManufactureId = productApiDto.ManufactureId,
                Manufacture = await GetManufactureDbAsync(productApiDto.ManufactureId),
            };

            _appDbContext.Products.Add(newProduct);
            await _appDbContext.SaveChangesAsync();

            return newProduct;
        }

        public async Task<Product> UpdateProduct(ProductUpdateApi productApiUpdateDto)
        {
            var productToUpdate = await GetProductDbAsync(productApiUpdateDto.OldName);

            if (productToUpdate is null)
            {
                throw new NotFoundException($"No products with name: [{productApiUpdateDto.OldName}] have been found");
            }

            var possibleExistingProduct = await GetProductDbAsync(productApiUpdateDto.NewName);

            if (possibleExistingProduct is not null) 
            {
                throw new ConflictException($"Product with name: [{productApiUpdateDto.NewName}] exists in the DB already");
            }

            var manufactureToUpdate = await GetManufactureDbAsync(productApiUpdateDto.NewManufactureId);

            if (manufactureToUpdate is null)
            {
                throw new NotFoundException($"No manufactures with ID: [{productApiUpdateDto.NewManufactureId}] have been found");
            }

            productToUpdate.Description = productApiUpdateDto.NewDescription;
            productToUpdate.Price = productApiUpdateDto.NewPrice;
            productToUpdate.ManufactureId = productApiUpdateDto.NewManufactureId;
            productToUpdate.Manufacture = manufactureToUpdate;

            await _appDbContext.SaveChangesAsync();

            return productToUpdate;
        }

        public async Task DeleteAllProducts()
        {
            await _appDbContext.Products.ForEachAsync(product =>
            {
                _appDbContext.Products.Remove(product);
            });
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Product> DeleteProduct(string name)
        {
            var productToDelete = await GetProductDbAsync(name);

            if (productToDelete is null)
            {
                throw new NotFoundException($"No products with name: [{name}] were found in the DB");
            }

            _appDbContext.Products.Remove(productToDelete);
            await _appDbContext.SaveChangesAsync();

            return productToDelete;
        }

        public async Task<List<Product>> GetProductsByManufactureName(string manufactureName)
        {
            return await _appDbContext.Products
                .Include(product => product.Manufacture)
                .Where(product => product.Manufacture.Name.ToLower() == manufactureName.ToLower())
                .ToListAsync();
        }

        private async Task<Manufacture> GetManufactureDbAsync(int manufactureId)
        {
            return await _appDbContext.Manufactures
                .FirstOrDefaultAsync(manufactue => manufactue.Id == manufactureId);
        }

        private async Task<Product> GetProductDbAsync(string productName)
        {
            return await _appDbContext.Products
                .Include(product => product.Manufacture)
                .FirstOrDefaultAsync(product => product.Name.ToLower() == productName.ToLower());
        }
    }
}
