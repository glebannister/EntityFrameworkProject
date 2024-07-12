using GlobalMarket.Core.ManufactureService;
using GlobalMarket.Core.Models.Api;
using Microsoft.AspNetCore.Mvc;

namespace GlobalMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufactureController : ControllerBase
    {
        private readonly IManufatureService _iManufatureService;

        public ManufactureController(IManufatureService iManufatureService)
        {
            _iManufatureService = iManufatureService;
        }

        [HttpGet("GetManufactureProducts")]
        public async Task<IActionResult> GetManufactureProducts(string manufactureName)
        {
            var listOfProducts = await _iManufatureService.GetManufactureProducts(manufactureName);

            return Ok(listOfProducts);
        }

        [HttpPost("AddManufacture")]
        public async Task<IActionResult> AddManufacture(ManufactureApi manufactureDto)
        {
            var manufactureToAdd = await _iManufatureService.AddManufacture(manufactureDto);

            return Ok(manufactureToAdd);
        }

        [HttpPut("UpdateManufacture")]
        public async Task<IActionResult> UpdateManufacture(ManufactureUpdateApi manufactureDtoUpdate)
        {
            var manufactureToUpdate = await _iManufatureService.UpdateManufacture(manufactureDtoUpdate);

            return Ok(manufactureToUpdate);
        }

        [HttpDelete("DeleteManufacture")]
        public async Task<IActionResult> DeleteManufacture(string manufactureName)
        {
            var manufactureToDelete = await _iManufatureService.DeleteManufacture(manufactureName);

            return Ok(manufactureToDelete);
        }

        [HttpDelete("DeleteAllManufactures")]
        public async Task<IActionResult> DeleteAllManufactures()
        {
            await _iManufatureService.DeleteAllManufactures();

            return Ok();
        }
    }
}
