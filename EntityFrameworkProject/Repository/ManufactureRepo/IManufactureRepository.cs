using EntityFrameworkProject.Entities.Model;

namespace EntityFrameworkProject.Repository.ManufactureRepo
{
    public interface IManufactureRepository
    {
        public Task AddAsync(Manufacture manufacture);

        public Task RemoveAsync(Manufacture manufacture);

        public Task<Manufacture> GetManufactureAsync(string manufactureName);

        public Task<List<Product>> GetManufactureProducts(string manufactureName);

        public Task SaveChangesAsync();

        public Task DeleteAllAsync();
    }
}
