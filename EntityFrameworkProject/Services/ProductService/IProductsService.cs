using EntityFrameworkProject.Entities.Dto;
using EntityFrameworkProject.Entities.Model;

namespace EntityFrameworkProject.Services.ProductSerivce
{
    public interface IProductsService
    {
        public Task<Product> AddProduct(ProductApiDto productApiDto);

        public Task<Product> UpdateProduct(ProductApiDto productApiDto);

        public Task<Product> DeleteProduct(string name);

        public  Task DeleteAllProducts();

        public Task<List<Product>> GetProductsByManufactureName(string manufactureName);
    }
}
