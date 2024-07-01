using EntityFrameworkProject.Entities.Dto;
using EntityFrameworkProject.Entities.Model;

namespace EntityFrameworkProject.Services.ManufactureService
{
    public interface IManufatureService
    {
        public Task<Manufacture> AddManufacture(ManufactureDto manufactureDto);

        public Task<List<Product>> GetManufactureProducts(string manufactureName);

        public Task<Manufacture> DeleteManufacture(string manufactureName);

        public Task<bool> IsManufactureExist(string manufactureName);
    }
}
