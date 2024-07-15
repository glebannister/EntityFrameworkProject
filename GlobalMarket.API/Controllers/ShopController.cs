using GlobalMarket.Core.Models.Api;
using GlobalMarket.Core.Services.ShopService;
using Microsoft.AspNetCore.Mvc;

namespace GlobalMarket.API.Controllers
{
    [Route("globalMarket/api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _iShopService;

        public ShopController(IShopService iShopService)
        {
            _iShopService = iShopService;
        }

        [HttpGet("{shopName}")]
        public async Task<IActionResult> GetProductsFromShop(string shopName)
        {
            var productsFromShop = await _iShopService.GetProductsFromShop(shopName);

            return Ok(productsFromShop);
        }

        [HttpGet("{productName}")]
        public async Task<IActionResult> GetShopsWithProduct(string productName)
        {
            var shopsWithProduct = await _iShopService.GetShopsWithProducts(productName);

            return Ok(shopsWithProduct);
        }

        [HttpPost]
        public async Task<IActionResult> AddShop(ShopApi shopApiDto)
        {
            var shopToAdd = await _iShopService.AddShop(shopApiDto);

            return Ok(shopToAdd);
        }

        [HttpPost("{productName}/{shopName}")]
        public async Task<IActionResult> AddProductToShop(string productName, string shopName)
        {
            var productShop = await _iShopService.AddProductToShop(productName, shopName);

            return Ok(productShop);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateShop(ShopUpdateApi shopApiUpdateDto)
        {
            var shopUpdate = await _iShopService.UpdateShop(shopApiUpdateDto);

            return Ok(shopUpdate);
        }

        [HttpDelete("{shopName}")]
        public async Task<IActionResult> DeleteShop(string name)
        {
            var shopDelete = await _iShopService.DeleteShop(name);

            return Ok(shopDelete);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllShops()
        {
            await _iShopService.DeleteAllShops();

            return Ok();
        }
    }
}
