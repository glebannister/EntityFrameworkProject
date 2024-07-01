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

            var products = await _iProductRepository.GetProductsAsync();
            var productToUpdate = products
                .First(product => product.Name.ToLower() == productApiDto.Name.ToLower());

            productToUpdate.Description = productApiDto.Description;
            productToUpdate.Price = productApiDto.Price;
            productToUpdate.ManufactureId = productApiDto.ManufactureId;
            productToUpdate.Manufacture = await _iProductRepository.GetManufactureAsync(productApiDto.ManufactureId);

            await _iProductRepository.SaveChangesAsync();

            return productToUpdate;
        }

        public async Task<Product> DeleteProduct(string name)
        {
            if (!await IsProductExist(name))
            {
                throw new NotFoundException($"No products with name: {name} were found in the DB");
            }

            var productToDelete = _iProductRepository.GetProductAsync(name).Result;
            await _iProductRepository.DeleteAsync(productToDelete);

            return productToDelete;
        }

        public async Task<List<Product>> GetProductsByManufactureName(string manufactureName)
        {
            var listOfAllProducts = await _iProductRepository.GetProductsAsync();

            return listOfAllProducts
                .Where(product => product.Manufacture.Name.ToLower() == manufactureName.ToLower()).ToList();
        }

        public async Task<bool> IsProductExist(string name)
        {
            var products = await _iProductRepository.GetProductsAsync();

            return products.Any(product => product.Name.ToLower() == name.ToLower());
        }

        public async Task<bool> IsManufactureExists(int manufactureId)
        {
            var manufacture = await _iProductRepository.GetManufactureAsync(manufactureId);

            return manufacture is not null;
        }
    }
}
