using GlobalMarket.Core.Models.Api;
using GlobalMarket.Core.Services.ProductSerivce;
using Microsoft.AspNetCore.Mvc;

namespace GlobalMarket.API.Controllers
{
    [Route("globalMarket/api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _iProductsService;

        public ProductsController(IProductsService productsService)
        {
            _iProductsService = productsService;
        }

        [HttpGet("{manufactureName}")]
        public async Task<IActionResult> GetProductsByManufactureName(string manufactureName) 
        {
            var listOfProducts = await _iProductsService.GetProductsByManufactureName(manufactureName);

            if (!listOfProducts.Any()) 
            {
                return NotFound($"No products fabricated by manufacture : {manufactureName} have been found");
            }

            return Ok(listOfProducts);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductApi productApiDto) 
        {
            var addedProduct = await _iProductsService.AddProduct(productApiDto);

            return Ok(addedProduct);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ProductUpdateApi productApiUpdateDto)
        {
            var productToUpdate = await _iProductsService.UpdateProduct(productApiUpdateDto);

            return Ok(productToUpdate);
        }

        [HttpDelete("{productName}")]
        public async Task<IActionResult> DeleteProduct(string name)
        {
            await _iProductsService.DeleteProduct(name);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllProducts()
        {
            await _iProductsService.DeleteAllProducts();

            return Ok();
        }
    }
}
