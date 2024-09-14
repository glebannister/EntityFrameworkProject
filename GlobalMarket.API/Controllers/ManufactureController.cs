using GlobalMarket.Core.Services.Interfaces;
using GlobalMarket.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GlobalMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufactureController : ControllerBase
    {
        private readonly IManufatureService _manufatureService;

        public ManufactureController(IManufatureService manufatureService)
        {
            _manufatureService = manufatureService;
        }

        [HttpGet("get-manufacture-products/{manufactureName}")]
        public async Task<IActionResult> GetManufactureProducts(string manufactureName)
        {
            var listOfProducts = await _manufatureService.GetManufactureProducts(manufactureName);

            return Ok(listOfProducts);
        }

        [HttpPost("add-manufacture")]
        public async Task<IActionResult> AddManufacture(ManufactureCreateDto manufactureCreateDto)
        {
            var manufactureToAdd = await _manufatureService.AddManufacture(manufactureCreateDto);

            return Ok(manufactureToAdd);
        }

        [HttpPut("update-manufacture")]
        public async Task<IActionResult> UpdateManufacture(ManufactureUpdateDto manufactureUpdateDto)
        {
            var manufactureToUpdate = await _manufatureService.UpdateManufacture(manufactureUpdateDto);

            return Ok(manufactureToUpdate);
        }

        [HttpDelete("delete-manufacture/{manufactureName}")]
        public async Task<IActionResult> DeleteManufacture(string manufactureName)
        {
            var manufactureToDelete = await _manufatureService.DeleteManufacture(manufactureName);

            return Ok(manufactureToDelete);
        }

        [HttpDelete("delete-all-manufactures")]
        public async Task<IActionResult> DeleteAllManufactures()
        {
            await _manufatureService.DeleteAllManufactures();

            return Ok();
        }
    }
}
