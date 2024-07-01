using EntityFrameworkProject.Entities.Dto;
using EntityFrameworkProject.Entities.Model;
using EntityFrameworkProject.Exceptions;
using EntityFrameworkProject.Repository.ManufactureRepo;

namespace EntityFrameworkProject.Services.ManufactureService
{
    public class ManufactureService : IManufatureService
    {
        private readonly IManufactureRepository _iManufactureRepository;

        public ManufactureService(IManufactureRepository iManufactureRepository)
        {
            _iManufactureRepository = iManufactureRepository;
        }

        public async Task<Manufacture> AddManufacture(ManufactureDto manufactureDto)
        {
            if (await IsManufactureExist(manufactureDto.Name))
            {
                throw new ConflictException($"The manufacture with name: {manufactureDto.Name} exists in the DB already");
            }

            var manufactureToAdd = new Manufacture
            {
                Address = manufactureDto.Address,
                Name = manufactureDto.Name,
            };

            await _iManufactureRepository.AddAsync(manufactureToAdd);

            return manufactureToAdd;
        }

        public async Task<List<Product>> GetManufactureProducts(string manufactureName)
        {
            var listOfProducts = await _iManufactureRepository.GetManufactureProducts(manufactureName);

            if (!listOfProducts.Any())
            {
                throw new NotFoundException($"No manufactures with name {manufactureName} have been found");
            }

            return listOfProducts;
        }

        public async Task<Manufacture> DeleteManufacture(string manufactureName)
        {
            if (!await IsManufactureExist(manufactureName))
            {
                throw new NotFoundException($"No manufactures with name {manufactureName} have been found");
            }

            var manufactureToDelete = await _iManufactureRepository.GetManufactureAsync(manufactureName);

            await _iManufactureRepository.RemoveAsync(manufactureToDelete);

            return manufactureToDelete;
        }

        public async Task<bool> IsManufactureExist(string manufactureName)
        {
            return await _iManufactureRepository.GetManufactureAsync(manufactureName) is not null;
        }
    }
}
