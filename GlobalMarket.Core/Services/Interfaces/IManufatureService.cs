using GlobalMarket.Core.Models;
using GlobalMarket.Dto;

namespace GlobalMarket.Core.Services.Interfaces
{
    public interface IManufatureService
    {
        public Task<List<Product>> GetManufactureProducts(string manufactureName);

        public Task<Manufacture> AddManufacture(ManufactureCreateDto manufactureCreateDto);

        public Task<Manufacture> UpdateManufacture(ManufactureUpdateDto manufactureUpdateDto);

        public Task<Manufacture> DeleteManufacture(string manufactureName);

        public Task DeleteAllManufactures();
    }
}
