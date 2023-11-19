using Microsoft.EntityFrameworkCore;
using RentalCompany.Core.Data;
using RentalCompany.Domain.Entities;
using RentalCompany.Domain.Repositories;

namespace RentalCompany.Data.Repositories
{
    internal class RentItemRepository : IRentItemRepository
    {
        private readonly RentalCompanyContext _context;

        public RentItemRepository(RentalCompanyContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<RentItem> CreateAsync(RentItem item)
        {
            await _context.Set<RentItem>().AddAsync(item);
            return await Task.FromResult(item);
        }

        public async Task<List<RentItem>> GetAllAsync()
        {
            return await _context.Set<RentItem>().ToListAsync();
        }

        public async Task<List<RentItem>?> GetExpiredRents(int quantityToBeObtained = 10)
            => await _context.Set<RentItem>().Where(x => x.RentDueDate >= DateTime.UtcNow).ToListAsync();

        public void UpdateRange(List<RentItem> rentItems)
        {
            _context.Set<RentItem>().UpdateRange(rentItems);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context?.Dispose();
            }
        }
    }
}
