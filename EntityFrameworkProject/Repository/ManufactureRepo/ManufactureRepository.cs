using EntityFrameworkProject.Data;
using EntityFrameworkProject.Entities.Model;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkProject.Repository.ManufactureRepo
{
    public class ManufactureRepository : IManufactureRepository
    {
        private readonly AppDbContext _appDbContext;

        public ManufactureRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(Manufacture manufacture)
        {
            _appDbContext.Manufactures.Add(manufacture);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(Manufacture manufacture)
        {
            _appDbContext.Manufactures.Remove(manufacture);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Manufacture> GetManufactureAsync(string manufactureName)
        {
            return await _appDbContext.Manufactures
                .FirstOrDefaultAsync(manufacture => manufacture.Name.ToLower() == manufactureName.ToLower());
        }

        public async Task<List<Product>> GetManufactureProducts(string manufactureName)
        {
            return _appDbContext.Products
                .Where(product => product.Manufacture.Name.ToLower() == manufactureName.ToLower())
                .ToList();
        }

        public async Task SaveChangesAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAllAsync()
        {
            await _appDbContext.Manufactures.ForEachAsync(manufacture =>
            {
                _appDbContext.Manufactures.Remove(manufacture);
            });
        }
    }
}
