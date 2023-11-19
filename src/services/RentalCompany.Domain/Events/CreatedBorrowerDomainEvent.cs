using RentalCompany.Core.Contracts;
using RentalCompany.Domain.Entities;

namespace RentalCompany.Domain.Events
{
    public record CreatedBorrowerDomainEvent(Borrower Borrower) : IDomainEvent
    {

    }
}
