using EntityFrameworkProject.Entities.Dto;
using EntityFrameworkProject.Entities.Model;

namespace EntityFrameworkProject.Services.ManufactureService
{
    public interface IManufatureService
    {
        public Task<List<Product>> GetManufactureProducts(string manufactureName);

        public Task<Manufacture> AddManufacture(ManufactureApiDto manufactureDto);

        public Task<Manufacture> UpdateManufacture(ManufactureApiUpdateDto manufactureDto);

        public Task<Manufacture> DeleteManufacture(string manufactureName);

        public Task DeleteAllManufactures();
    }
}
