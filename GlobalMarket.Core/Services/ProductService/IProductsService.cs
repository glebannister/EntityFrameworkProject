using GlobalMarket.Core.Models.Api;
using GlobalMarket.Core.Models.Database;

namespace GlobalMarket.Core.Services.ProductSerivce
{
    public interface IProductsService
    {
        public Task<Product> AddProduct(ProductApi productApiDto);

        public Task<Product> UpdateProduct(ProductUpdateApi productApiUpdateDto);

        public Task<Product> DeleteProduct(string name);

        public  Task DeleteAllProducts();

        public Task<List<Product>> GetProductsByManufactureName(string manufactureName);
    }
}
