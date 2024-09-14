using GlobalMarket.Core.Dto;
using GlobalMarket.Core.Models;
using GlobalMarket.Dto;

namespace GlobalMarket.Core.Services.Interfaces
{
    public interface IProductsService
    {
        public Task<Product> AddProduct(ProductCreateDto productCreateDto);

        public Task<Product> UpdateProduct(ProductUpdateDto productUpdateDto);

        public Task<Product> DeleteProduct(string name);

        public Task DeleteAllProducts();

        public Task<List<GetProductDto>> GetProductsByManufactureName(string manufactureName);
    }
}
