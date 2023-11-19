namespace RentalCompany.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit(CancellationToken cancellationToken = default);
    }
}
