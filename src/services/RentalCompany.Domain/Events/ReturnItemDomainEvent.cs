using RentalCompany.Core.Contracts;
using RentalCompany.Domain.Entities;

namespace RentalCompany.Domain.Events
{
    public sealed class ReturnItemDomainEvent : IDomainEvent
    {
        internal ReturnItemDomainEvent(Guid borrowerId, CollectionItem item, int quantityReturned)
        {
            BorrowerId = borrowerId;
            Item = item;
            QuantityReturned = quantityReturned;
        }

        public int QuantityReturned { get; set; }
        public Guid BorrowerId { get; set; }
        public CollectionItem Item { get; set; }
    }
}