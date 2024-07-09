using EntityFrameworkProject.Entities.Dto;
using EntityFrameworkProject.Services.ShopService;

using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _iShopService;

        public ShopController(IShopService iShopService)
        {
            _iShopService = iShopService;
        }

        [HttpGet("GetProductsFromShop")]
        public async Task<IActionResult> GetProductsFromShop(string shopName)
        {
            var productsFromShop = await _iShopService.GetProductsFromShop(shopName);

            return Ok(productsFromShop);
        }

        [HttpGet("GetShopsWithProduct")]
        public async Task<IActionResult> GetShopsWithProduct(string productName)
        {
            var shopsWithProduct = await _iShopService.GetShopsWithProducts(productName);

            return Ok(shopsWithProduct);
        }

        [HttpPost("AddShop")]
        public async Task<IActionResult> AddShop(ShopApiDto shopApiDto)
        {
            var shopToAdd = await _iShopService.AddShop(shopApiDto);

            return Ok(shopToAdd);
        }

        [HttpPost("AddProductToShop")]
        public async Task<IActionResult> AddProductToShop(string productName, string shopName)
        {
            var productShop = await _iShopService.AddProductToShop(productName, shopName);

            return Ok(productShop);
        }

        [HttpPut("UpdateShop")]
        public async Task<IActionResult> UpdateShop(ShopApiUpdateDto shopApiUpdateDto)
        {
            var shopUpdate = await _iShopService.UpdateShop(shopApiUpdateDto);

            return Ok(shopUpdate);
        }

        [HttpDelete("DeleteShop")]
        public async Task<IActionResult> DeleteShop(string name)
        {
            var shopDelete = await _iShopService.DeleteShop(name);

            return Ok(shopDelete);
        }

        [HttpDelete("DeleteAllShops")]
        public async Task<IActionResult> DeleteAllShops()
        {
            await _iShopService.DeleteAllShops();

            return Ok();
        }
    }
}
