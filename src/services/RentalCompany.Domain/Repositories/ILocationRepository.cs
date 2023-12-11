using RentalCompany.Core.Data;
using RentalCompany.Domain.Entities;

namespace RentalCompany.Domain.Repositories
{
    public interface ILocationRepository : IRepository<Location>
    {
        Task CreateAsync(Location location);
        void Delete(Location location);
        void Update(Location location);
        Task<List<Location>> GetRootsAsync();
        Task<List<Location>?> GetLocationsChildrenAsync(Guid id);
        Task<Location?> GetByIdAsync(Guid id);
        Task<string> GetFullLocationTag(Guid id);
    }
}
