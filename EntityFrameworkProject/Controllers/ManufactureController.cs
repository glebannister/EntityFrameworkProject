using EntityFrameworkProject.Entities.Dto;
using EntityFrameworkProject.Services.ManufactureService;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkProject.Controllers
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

        [HttpPost]
        public async Task<IActionResult> AddManufacture(ManufactureDto manufactureDto)
        {
            var manufactureToAdd = await _iManufatureService.AddManufacture(manufactureDto);

            return Ok(manufactureToAdd);
        }

        [HttpGet]
        public async Task<IActionResult> GetManufactureProducts(string manufactureName) 
        {
            var listOfProducts = await _iManufatureService.GetManufactureProducts(manufactureName);

            return Ok(listOfProducts);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteManufacture(string manufactureName)
        {
            var manufactureToDelete = await _iManufatureService.DeleteManufacture(manufactureName);

            return Ok(manufactureToDelete);
        }
    }
}
