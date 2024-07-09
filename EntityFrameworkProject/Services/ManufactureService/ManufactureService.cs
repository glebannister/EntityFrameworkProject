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

        public async Task<Manufacture> AddManufacture(ManufactureApiDto manufactureDto)
        {
            var manufacture = await _iManufactureRepository.GetManufactureAsync(manufactureDto.Name);

            if (manufacture is not null)
            {
                throw new ConflictException($"The manufacture with name: [{manufactureDto.Name}] exists in the DB already");
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
                throw new NotFoundException($"No manufactures with name [{manufactureName}] have been found");
            }

            return listOfProducts;
        }

        public async Task<Manufacture> DeleteManufacture(string manufactureName)
        {
            var manufactureToDelete = await _iManufactureRepository.GetManufactureAsync(manufactureName);

            if (manufactureToDelete is null)
            {
                throw new NotFoundException($"No manufactures with name [{manufactureName}] have been found");
            }

            await _iManufactureRepository.RemoveAsync(manufactureToDelete);

            return manufactureToDelete;
        }

        public async Task<Manufacture> UpdateManufacture(ManufactureApiUpdateDto manufactureDtoUpdate)
        {
            var manufactureToUpdate = await _iManufactureRepository.GetManufactureAsync(manufactureDtoUpdate.OldName);

            if (manufactureToUpdate is null)
            {
                throw new NotFoundException($"No manufactures with name [{manufactureDtoUpdate.OldName}] have been found");
            }

            var possibleUpdatedManufacture = await _iManufactureRepository.GetManufactureAsync(manufactureDtoUpdate.NewName);

            if (possibleUpdatedManufacture is not null)
            {
                throw new ConflictException($"Manufacture with name [{manufactureDtoUpdate.NewName}] exists in the DB already");
            }

            manufactureToUpdate.Name = manufactureDtoUpdate.NewName;
            manufactureToUpdate.Address = manufactureDtoUpdate.NewAddress;

            await _iManufactureRepository.SaveChangesAsync();

            return manufactureToUpdate;
        }

        public async Task DeleteAllManufactures()
        {
            await _iManufactureRepository.DeleteAllAsync();
        }
    }
}
