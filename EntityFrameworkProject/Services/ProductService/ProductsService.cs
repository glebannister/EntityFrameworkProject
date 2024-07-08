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
            var productToAdd = await _iProductRepository.GetProductAsync(productApiDto.Name);

            if (productToAdd is null)
            {
                throw new ConflictException($"Prodcut with the name {productApiDto.Name} exists in the DB already");
            }

            var manufacture = await _iProductRepository.GetManufactureAsync(productApiDto.ManufactureId);

            if (manufacture is null)
            {
                throw new NotFoundException($"No manufactues with ID {productApiDto.ManufactureId} have been found");
            }

            Product newProduct = new Product
            {
                Name = productApiDto.Name,
                Description = productApiDto.Description,
                Price = productApiDto.Price,
                ManufactureId = productApiDto.ManufactureId,
                Manufacture = await _iProductRepository.GetManufactureAsync(productApiDto.ManufactureId),
            };

            await _iProductRepository.AddAsync(newProduct);

            return newProduct;
        }

        public async Task<Product> UpdateProduct(ProductApiDto productApiDto)
        {
            var productToUpdate = await _iProductRepository.GetProductAsync(productApiDto.Name);

            if (productToUpdate is null)
            {
                throw new NotFoundException($"No products with ID: {productApiDto.Name} have been found");
            }

            var manufactureToUpdate = await _iProductRepository.GetManufactureAsync(productApiDto.ManufactureId);

            if (manufactureToUpdate is null)
            {
                throw new NotFoundException($"No manufactures with ID: {productApiDto.Name} have been found");
            }

            productToUpdate.Description = productApiDto.Description;
            productToUpdate.Price = productApiDto.Price;
            productToUpdate.ManufactureId = productApiDto.ManufactureId;
            productToUpdate.Manufacture = manufactureToUpdate;

            await _iProductRepository.SaveChangesAsync();

            return productToUpdate;
        }

        public async Task DeleteAllProducts()
        {
            await _iProductRepository.DeleteAllAsync();
        }

        public async Task<Product> DeleteProduct(string name)
        {
            var productToDelete = await _iProductRepository.GetProductAsync(name);

            if (productToDelete is null)
            {
                throw new NotFoundException($"No products with name: {name} were found in the DB");
            }

            await _iProductRepository.DeleteAsync(productToDelete);

            return productToDelete;
        }

        public async Task<List<Product>> GetProductsByManufactureName(string manufactureName)
        {
            return await _iProductRepository.GetProductsAsync(manufactureName);
        }
    }
}
