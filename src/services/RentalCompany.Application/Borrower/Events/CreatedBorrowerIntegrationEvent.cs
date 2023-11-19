using RentalCompany.Core.Contracts;
using RentalCompany.Domain.Events;

namespace RentalCompany.Application.Borrower.Events
{
    public sealed class CreatedBorrowerIntegrationEvent : IIntegrationEvent
    {
        public CreatedBorrowerIntegrationEvent(Guid borrowerId) => BorrowerId = borrowerId;

        internal CreatedBorrowerIntegrationEvent(CreatedBorrowerDomainEvent domainEvent) => BorrowerId = domainEvent.Borrower.Id;

        public Guid BorrowerId { get; set; }
    }
}
