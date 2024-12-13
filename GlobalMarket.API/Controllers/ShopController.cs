using GlobalMarket.Core.Services.Interfaces;
using GlobalMarket.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GlobalMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }

        [HttpGet("get-products-from-shop/{shopName}")]
        public async Task<IActionResult> GetProductsFromShop(string shopName)
        {
            var productsFromShop = await _shopService.GetProductsFromShop(shopName);

            return Ok(productsFromShop);
        }

        [HttpGet("get-shops-with-product/{productName}")]
        public async Task<IActionResult> GetShopsWithProduct(string productName)
        {
            var shopsWithProduct = await _shopService.GetShopsWithProducts(productName);

            return Ok(shopsWithProduct);
        }

        [HttpPost("add-shop")]
        public async Task<IActionResult> AddShop(ShopCreateDto shopCreateDto)
        {
            var shopToAdd = await _shopService.AddShop(shopCreateDto);

            return Ok(shopToAdd);
        }

        [HttpPost("add-productToShop/{productName}/{shopName}")]
        public async Task<IActionResult> AddProductToShop(string productName, string shopName)
        {
            var productShop = await _shopService.AddProductToShop(productName, shopName);

            return Ok(productShop);
        }

        [HttpPut("update-shop")]
        public async Task<IActionResult> UpdateShop(ShopUpdateDto shopUpdateDto)
        {
            var shopUpdate = await _shopService.UpdateShop(shopUpdateDto);

            return Ok(shopUpdate);
        }

        [HttpDelete("delete-shop/{shopName}")]
        public async Task<IActionResult> DeleteShop(string name)
        {
            var shopDelete = await _shopService.DeleteShop(name);

            return Ok(shopDelete);
        }

        [HttpDelete("delete-all-shops")]
        public async Task<IActionResult> DeleteAllShops()
        {
            await _shopService.DeleteAllShops();

            return Ok();
        }
    }
}
