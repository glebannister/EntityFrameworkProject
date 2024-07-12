using GlobalMarket.Core.Models.Api;
using GlobalMarket.Core.Models.Database;

namespace GlobalMarket.Core.ManufactureService
{
    public interface IManufatureService
    {
        public Task<List<Product>> GetManufactureProducts(string manufactureName);

        public Task<Manufacture> AddManufacture(ManufactureApi manufactureDto);

        public Task<Manufacture> UpdateManufacture(ManufactureUpdateApi manufactureDto);

        public Task<Manufacture> DeleteManufacture(string manufactureName);

        public Task DeleteAllManufactures();
    }
}
