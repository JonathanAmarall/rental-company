using RentalCompany.Core.Data;
using RentalCompany.Domain.Entities;

namespace RentalCompany.Domain.Repositories
{
    public interface IRentItemRepository : IRepository<RentItem>
    {
        Task<RentItem> CreateAsync(RentItem item);

        Task<List<RentItem>> GetAllAsync();

        Task<List<RentItem>?> GetExpiredRents(int quantityToBeObtained = 10);

        void UpdateRange(List<RentItem> rentItems);
    }
}
