using EntityFrameworkProject.Entities.Dto;
using EntityFrameworkProject.Services.ProductSerivce;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _iProductsService;

        public ProductsController(IProductsService productsService)
        {
            _iProductsService = productsService;
        }

        [HttpGet("GetProductsByManufactureName")]
        public async Task<IActionResult> GetProductsByManufactureName(string manufactureName) 
        {
            var listOfProducts = await _iProductsService.GetProductsByManufactureName(manufactureName);

            if (!listOfProducts.Any()) 
            {
                return NotFound($"No products fabricated by manufacture : {manufactureName} have been found");
            }

            return Ok(listOfProducts);
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(ProductApiDto productApiDto) 
        {
            var addedProduct = await _iProductsService.AddProduct(productApiDto);

            return Ok(addedProduct);
        }

        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(string name)
        {
            await _iProductsService.DeleteProduct(name);

            return Ok();
        }

        [HttpDelete("DeleteAllProducts")]
        public async Task<IActionResult> DeleteAllProducts()
        {
            await _iProductsService.DeleteAllProducts();

            return Ok();
        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(ProductApiDto productApiDto) 
        {
            var productToUpdate = await _iProductsService.UpdateProduct(productApiDto);

            return Ok(productToUpdate);
        }
    }
}
