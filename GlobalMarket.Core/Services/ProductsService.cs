using GlobalMarket.Core.Dto;
using GlobalMarket.Core.Exceptions;
using GlobalMarket.Core.Models;
using GlobalMarket.Core.Repository;
using GlobalMarket.Core.Services.Interfaces;
using GlobalMarket.Dto;
using Microsoft.EntityFrameworkCore;

namespace GlobalMarket.Core.Services
{
    public class ProductsService : IProductsService
    {
        private readonly AppDbContext _appDbContext;

        public ProductsService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Product> AddProduct(ProductCreateDto productCreateDto)
        {
            var productToAdd = await GetProductDbAsync(productCreateDto.Name);

            if (productToAdd is null)
            {
                throw new AlreadyExistException($"Prodcut with the name [{productCreateDto.Name}] exists in the DB already");
            }

            var manufacture = await GetManufactureDbAsync(productCreateDto.ManufactureId);

            if (manufacture is null)
            {
                throw new NotFoundException($"No manufactues with ID [{productCreateDto.ManufactureId}] have been found");
            }

            Product newProduct = new Product
            {
                Name = productCreateDto.Name,
                Description = productCreateDto.Description,
                Price = productCreateDto.Price,
                ManufactureId = productCreateDto.ManufactureId,
                Manufacture = await GetManufactureDbAsync(productCreateDto.ManufactureId),
            };

            _appDbContext.Products.Add(newProduct);
            await _appDbContext.SaveChangesAsync();

            return newProduct;
        }

        public async Task<Product> UpdateProduct(ProductUpdateDto productUpdateDto)
        {
            var productToUpdate = await GetProductDbAsync(productUpdateDto.OldName);

            if (productToUpdate is null)
            {
                throw new NotFoundException($"No products with name: [{productUpdateDto.OldName}] have been found");
            }

            var possibleExistingProduct = await GetProductDbAsync(productUpdateDto.NewName);

            if (possibleExistingProduct is not null)
            {
                throw new AlreadyExistException($"Product with name: [{productUpdateDto.NewName}] exists in the DB already");
            }

            var manufactureToUpdate = await GetManufactureDbAsync(productUpdateDto.NewManufactureId);

            if (manufactureToUpdate is null)
            {
                throw new NotFoundException($"No manufactures with ID: [{productUpdateDto.NewManufactureId}] have been found");
            }

            productToUpdate.Description = productUpdateDto.NewDescription;
            productToUpdate.Price = productUpdateDto.NewPrice;
            productToUpdate.ManufactureId = productUpdateDto.NewManufactureId;
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

        public async Task<List<GetProductDto>> GetProductsByManufactureName(string manufactureName)
        {
            var products = await _appDbContext.Products
                .Include(product => product.Manufacture)
                .Where(product => product.Manufacture.Name.ToLower() == manufactureName.ToLower())
                .ToListAsync();

            var result = products.Select(product => new GetProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Manufacture = product.Manufacture
            }).ToList();

            return result;
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
