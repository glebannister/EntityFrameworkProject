using EntityFrameworkProject.Entities.Dto;
using EntityFrameworkProject.Services.ShopService;
using Microsoft.AspNetCore.Http;
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

        //[HttpPost]
        //public async Task<IActionResult> AddShop(ShopApiDto shopApiDto)
        //{
        //    var shopToAdd = await _iShopService.AddShop(shopApiDto);

        //    return Ok(shopToAdd);
        //}

        [HttpPost]
        public async Task<IActionResult> AddProductToShop(string productName, string shopName)
        {
            var productShop = await _iShopService.AddProductToShop(productName, shopName);

            return Ok(productShop);
        }
    }
}
