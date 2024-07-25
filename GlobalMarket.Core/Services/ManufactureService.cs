using GlobalMarket.Core.Exceptions;
using GlobalMarket.Core.Models;
using GlobalMarket.Core.Repository;
using GlobalMarket.Core.Services.Interfaces;
using GlobalMarket.Dto;
using Microsoft.EntityFrameworkCore;

namespace GlobalMarket.Core.Services
{
    public class ManufactureService : IManufatureService
    {
        private readonly AppDbContext _appDbContext;

        public ManufactureService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Manufacture> AddManufacture(ManufactureCreateDto manufactureCreateDto)
        {
            var manufacture = await GetManufactureDbAsync(manufactureCreateDto.Name);

            if (manufacture is not null)
            {
                throw new AlreadyExistException($"The manufacture with name: [{manufactureCreateDto.Name}] exists in the DB already");
            }

            var manufactureToAdd = new Manufacture
            {
                Address = manufactureCreateDto.Address,
                Name = manufactureCreateDto.Name,
            };

            _appDbContext.Manufactures.Add(manufacture);
            await _appDbContext.SaveChangesAsync();

            return manufactureToAdd;
        }

        public async Task<List<Product>> GetManufactureProducts(string manufactureName)
        {
            var listOfProducts = _appDbContext.Products
                .Where(product => product.Manufacture.Name.ToLower() == manufactureName.ToLower())
                .Include(product => product.Manufacture)
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

        public async Task<Manufacture> UpdateManufacture(ManufactureUpdateDto manufactureUpdateDto)
        {
            var manufactureToUpdate = await GetManufactureDbAsync(manufactureUpdateDto.OldName);

            if (manufactureToUpdate is null)
            {
                throw new NotFoundException($"No manufactures with name [{manufactureUpdateDto.OldName}] have been found");
            }

            var possibleUpdatedManufacture = await GetManufactureDbAsync(manufactureUpdateDto.NewName);

            if (possibleUpdatedManufacture is not null)
            {
                throw new AlreadyExistException($"Manufacture with name [{manufactureUpdateDto.NewName}] exists in the DB already");
            }

            manufactureToUpdate.Name = manufactureUpdateDto.NewName;
            manufactureToUpdate.Address = manufactureUpdateDto.NewAddress;

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
