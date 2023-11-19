using Microsoft.EntityFrameworkCore;
using RentalCompany.Core.Data;
using RentalCompany.Core.DTOs;
using RentalCompany.Data.Extensions;
using RentalCompany.Domain.Entities;
using RentalCompany.Domain.Repositories;

namespace RentalCompany.Data.Repositories
{
    public class CollectionItemRepository : ICollectionItemRepository
    {
        private readonly RentalCompanyContext _context;

        public CollectionItemRepository(RentalCompanyContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task CreateAsync(CollectionItem item)
        {
            await _context.CollectionItems!.AddAsync(item);
        }

        public void Delete(CollectionItem item)
        {
            _context.CollectionItems!.Remove(item);
        }

        public async Task<PagedList<CollectionItem>?> GetAllPagedAsync(string? globalFilter, string? sortOrder, string? sortField, ECollectionStatus? status, EType? type, int pageNumber = 1, int pageSize = 5)
        {
            var query = _context.CollectionItems!.Include(c => c.Rentals).AsQueryable();

            if (!string.IsNullOrWhiteSpace(globalFilter))
            {
                query = query.Where(x =>
                    x.Autor.ToUpper().Contains(globalFilter.ToUpper()) ||
                    x.Title.ToUpper().Contains(globalFilter.ToUpper()) ||
                    x.Edition!.ToUpper().Contains(globalFilter.ToUpper())
                );
            }

            if (!string.IsNullOrWhiteSpace(sortOrder) && !string.IsNullOrWhiteSpace(sortField))
            {
                query = query.GenericOrderBy(sortField, sortOrder.ToUpper() == "DESC");
            }

            if (status != null)
            {
                query = query.Where(x => x.Status == status);
            }

            if (type != null)
            {
                query = query.Where(x => x.ItemType == type);
            }

            return await query.ToPagedListAsync(pageNumber, pageSize);
        }

        public async Task<CollectionItem?> GetByIdAsync(Guid collectionItemId)
        {
            return await _context.CollectionItems!.FirstOrDefaultAsync(x => x.Id == collectionItemId);
        }

        public void Update(CollectionItem item)
        {
            _context.Entry(item).State = EntityState.Modified;
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
