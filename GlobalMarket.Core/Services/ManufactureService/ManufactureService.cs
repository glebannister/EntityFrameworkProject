using GlobalMarket.Core.Exceptions;
using GlobalMarket.Core.ManufactureService;
using GlobalMarket.Core.Models.Api;
using GlobalMarket.Core.Models.Database;
using GlobalMarket.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace GlobalMarket.Core.Services.ManufactureService
{
    public class ManufactureService : IManufatureService
    {
        private readonly AppDbContext _appDbContext;

        public ManufactureService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Manufacture> AddManufacture(ManufactureApi manufactureDto)
        {
            var manufacture = await GetManufactureDbAsync(manufactureDto.Name);

            if (manufacture is not null)
            {
                throw new ConflictException($"The manufacture with name: [{manufactureDto.Name}] exists in the DB already");
            }

            var manufactureToAdd = new Manufacture
            {
                Address = manufactureDto.Address,
                Name = manufactureDto.Name,
            };

            _appDbContext.Manufactures.Add(manufacture);
            await _appDbContext.SaveChangesAsync();

            return manufactureToAdd;
        }

        public async Task<List<Product>> GetManufactureProducts(string manufactureName)
        {
            var listOfProducts = _appDbContext.Products
                .Where(product => product.Manufacture.Name.ToLower() == manufactureName.ToLower())
                .ToList();

            if (!listOfProducts.Any())
            {
                throw new NotFoundException($"No manufactures with name [{manufactureName}] have been found");
            }

            return listOfProducts;
        }

        public async Task<Manufacture> DeleteManufacture(string manufactureName)
        {
            var manufactureToDelete = await GetManufactureDbAsync(manufactureName);

            if (manufactureToDelete is null)
            {
                throw new NotFoundException($"No manufactures with name [{manufactureName}] have been found");
            }

            _appDbContext.Manufactures.Remove(manufactureToDelete);
            await _appDbContext.SaveChangesAsync();

            return manufactureToDelete;
        }

        public async Task<Manufacture> UpdateManufacture(ManufactureUpdateApi manufactureDtoUpdate)
        {
            var manufactureToUpdate = await GetManufactureDbAsync(manufactureDtoUpdate.OldName);

            if (manufactureToUpdate is null)
            {
                throw new NotFoundException($"No manufactures with name [{manufactureDtoUpdate.OldName}] have been found");
            }

            var possibleUpdatedManufacture = await GetManufactureDbAsync(manufactureDtoUpdate.NewName);

            if (possibleUpdatedManufacture is not null)
            {
                throw new ConflictException($"Manufacture with name [{manufactureDtoUpdate.NewName}] exists in the DB already");
            }

            manufactureToUpdate.Name = manufactureDtoUpdate.NewName;
            manufactureToUpdate.Address = manufactureDtoUpdate.NewAddress;

            await _appDbContext.SaveChangesAsync();

            return manufactureToUpdate;
        }

        public async Task DeleteAllManufactures()
        {
            await _appDbContext.Manufactures.ForEachAsync(manufacture =>
            {
                _appDbContext.Manufactures.Remove(manufacture);
            });
        }

        private async Task<Manufacture> GetManufactureDbAsync(string manufactureName)
        {
            return await _appDbContext.Manufactures
                .FirstOrDefaultAsync(manufacture => manufacture.Name.ToLower() == manufactureName.ToLower());
        }
    }
}
