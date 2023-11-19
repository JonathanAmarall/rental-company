using RentalCompany.Core.Models;

namespace RentalCompany.Core.Data
{
    public interface IRepository<T> : IDisposable where T : AggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
