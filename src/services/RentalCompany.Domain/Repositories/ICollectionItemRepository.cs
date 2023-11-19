using RentalCompany.Core.Data;
using RentalCompany.Core.DTOs;
using RentalCompany.Domain.Entities;


namespace RentalCompany.Domain.Repositories
{
    public interface ICollectionItemRepository : IRepository<CollectionItem>
    {
        Task<PagedList<CollectionItem>?> GetAllPagedAsync(string? globalFilter, string? sortOrder, string? sortField, ECollectionStatus? status, EType? type, int pageNumber = 1, int pageSize = 5);
        Task CreateAsync(CollectionItem item);
        void Delete(CollectionItem item);
        void Update(CollectionItem item);
        Task<CollectionItem?> GetByIdAsync(Guid collectionItemId);
    }
}
