using EntityFrameworkProject.Entities.Dto;
using EntityFrameworkProject.Entities.Model;
using EntityFrameworkProject.Exceptions;
using EntityFrameworkProject.Repository.ProductRepo;
using EntityFrameworkProject.Services.ProductSerivce;

namespace EntityFrameworkProject.Services.ProductService
{
    public class ProductsService : IProductsService
    {
        private readonly IProductRepository _iProductRepository;

        public ProductsService(IProductRepository iProductRepository)
        {
            _iProductRepository = iProductRepository;
        }

        public async Task<Product> AddProduct(ProductApiDto productApiDto)
        {
            if (!await IsManufactureExists(productApiDto.ManufactureId))
            {
                throw new NotFoundException($"No manufactues with ID {productApiDto.ManufactureId} have been found");
            }

            if (await IsProductExist(productApiDto.Name))
            {
                throw new ConflictException($"Prodcut with the name {productApiDto.Name} exists in the DB already");
            }

            Product productToAdd = new Product
            {
                Name = productApiDto.Name,
                Description = productApiDto.Description,
                Price = productApiDto.Price,
                ManufactureId = productApiDto.ManufactureId,
                Manufacture = await _iProductRepository.GetManufactureAsync(productApiDto.ManufactureId),
            };

            await _iProductRepository.AddAsync(productToAdd);

            return productToAdd;
        }

        public async Task<Product> UpdateProduct(ProductApiDto productApiDto)
        {
            if (!await IsProductExist(productApiDto.Name))
            {
                throw new NotFoundException($"No products with ID: {productApiDto.Name} have been found");
            }

            if (!await IsManufactureExists(productApiDto.ManufactureId))
            {
                throw new NotFoundException($"No manufactures with ID: {productApiDto.Name} have been found");
            }

            var productToUpdate = await _iProductRepository.GetProductAsync(productApiDto.Name);

            productToUpdate.Description = productApiDto.Description;
            productToUpdate.Price = productApiDto.Price;
            productToUpdate.ManufactureId = productApiDto.ManufactureId;
            productToUpdate.Manufacture = await _iProductRepository.GetManufactureAsync(productApiDto.ManufactureId);

            await _iProductRepository.SaveChangesAsync();

            return productToUpdate;
        }

        public async Task DeleteAllProducts()
        {
            await _iProductRepository.DeleteAllAsync();
        }

        public async Task<Product> DeleteProduct(string name)
        {
            if (!await IsProductExist(name))
            {
                throw new NotFoundException($"No products with name: {name} were found in the DB");
            }

            var productToDelete = await _iProductRepository.GetProductAsync(name);
            await _iProductRepository.DeleteAsync(productToDelete);

            return productToDelete;
        }

        public async Task<List<Product>> GetProductsByManufactureName(string manufactureName)
        {
            return await _iProductRepository.GetProductsAsync(manufactureName);
        }

        public async Task<bool> IsProductExist(string name)
        {
            var product = await _iProductRepository.GetProductAsync(name);

            return product is not null;
        }

        public async Task<bool> IsManufactureExists(int manufactureId)
        {
            var manufacture = await _iProductRepository.GetManufactureAsync(manufactureId);

            return manufacture is not null;
        }
    }
}
