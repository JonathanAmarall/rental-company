using RentalCompany.Core.Data;
using RentalCompany.Core.DTOs;
using RentalCompany.Domain.Entities;

namespace RentalCompany.Domain.Repositories
{
    public interface IBorrowerRepository : IRepository<Borrower>
    {
        Task<Borrower?> GetByIdAsync(Guid borrowerId);
        Task<PagedList<Borrower>> GetAllPagedAsync(string? globalFilter, string? sortOrder, string? sortField, int pageNumber = 1, int pageSize = 5);
        Task<Borrower> CreateBorrowerAsync(Borrower borrower);
    }
}
