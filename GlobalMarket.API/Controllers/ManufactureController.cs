using GlobalMarket.Core.ManufactureService;
using GlobalMarket.Core.Models.Api;
using Microsoft.AspNetCore.Mvc;

namespace GlobalMarket.API.Controllers
{
    [Route("globalMarket/api/[controller]")]
    [ApiController]
    public class ManufactureController : ControllerBase
    {
        private readonly IManufatureService _iManufatureService;

        public ManufactureController(IManufatureService iManufatureService)
        {
            _iManufatureService = iManufatureService;
        }

        [HttpGet("{manufactureName}")]
        public async Task<IActionResult> GetManufactureProducts(string manufactureName)
        {
            var listOfProducts = await _iManufatureService.GetManufactureProducts(manufactureName);

            return Ok(listOfProducts);
        }

        [HttpPost]
        public async Task<IActionResult> AddManufacture(ManufactureApi manufactureDto)
        {
            var manufactureToAdd = await _iManufatureService.AddManufacture(manufactureDto);

            return Ok(manufactureToAdd);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateManufacture(ManufactureUpdateApi manufactureDtoUpdate)
        {
            var manufactureToUpdate = await _iManufatureService.UpdateManufacture(manufactureDtoUpdate);

            return Ok(manufactureToUpdate);
        }

        [HttpDelete("{manufactureName}")]
        public async Task<IActionResult> DeleteManufacture(string manufactureName)
        {
            var manufactureToDelete = await _iManufatureService.DeleteManufacture(manufactureName);

            return Ok(manufactureToDelete);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllManufactures()
        {
            await _iManufatureService.DeleteAllManufactures();

            return Ok();
        }
    }
}
