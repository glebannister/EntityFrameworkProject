using GlobalMarket.Core.Services.Interfaces;
using GlobalMarket.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GlobalMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet("get-products-by-manufacture-name/{manufactureName}")]
        public async Task<IActionResult> GetProductsByManufactureName(string manufactureName) 
        {
            var listOfProducts = await _productsService.GetProductsByManufactureName(manufactureName);

            if (!listOfProducts.Any()) 
            {
                return NotFound($"No products fabricated by manufacture : {manufactureName} have been found");
            }

            return Ok(listOfProducts);
        }

        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct(ProductCreateDto productCreateDto) 
        {
            var addedProduct = await _productsService.AddProduct(productCreateDto);

            return Ok(addedProduct);
        }

        [HttpPut("update-product")]
        public async Task<IActionResult> UpdateProduct(ProductUpdateDto productUpdateDto)
        {
            var productToUpdate = await _productsService.UpdateProduct(productUpdateDto);

            return Ok(productToUpdate);
        }

        [HttpDelete("delete-product/{productName}")]
        public async Task<IActionResult> DeleteProduct(string name)
        {
            await _productsService.DeleteProduct(name);

            return Ok();
        }

        [HttpDelete("delete-all-products")]
        public async Task<IActionResult> DeleteAllProducts()
        {
            await _productsService.DeleteAllProducts();

            return Ok();
        }
    }
}
