using RentalCompany.Core.Contracts;
using RentalCompany.Core.Exceptions;
using RentalCompany.Core.Models;
using RentalCompany.Domain.Events;

namespace RentalCompany.Domain.Entities
{
    public class RentItem : AggregateRoot, IAuditableEntity
    {
        private const int RentDueInDays = 5;

        public RentItem(int rentedQuantity, Guid collectionItemId, Guid borrowerId)
        {
            if (rentedQuantity <= 0)
            {
                throw new DomainException("Rent Quantity is invalid.");
            }

            RentDueDate = DateTime.Now.AddDays(RentDueInDays);
            RentedQuantity = rentedQuantity;
            CollectionItemId = collectionItemId;
            BorrowerId = borrowerId;
        }

        public int RentedQuantity { get; private set; }
        public int QuantityReturned { get; private set; }

        public DateTime RentDueDate { get; private set; }

        public Guid CollectionItemId { get; private set; }
        public CollectionItem CollectionItem { get; private set; } = null;

        public Guid BorrowerId { get; private set; }
        public Borrower Borrower { get; private set; } = null;

        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; }

        public bool AllItemsReturned() => RentedQuantity == QuantityReturned;

        public void ReturnItem(int quantityReturned)
        {
            if (quantityReturned > RentedQuantity)
            {
                throw new DomainException("Quantity Returned is invalid.");
            }

            QuantityReturned = quantityReturned;
            AddDomainEvent(new ReturnItemDomainEvent(BorrowerId, CollectionItem, quantityReturned));
        }

        public bool RentDueDateIsExpired()
        {
            var nowUtc = DateTime.UtcNow;
            return nowUtc >= RentDueDate;
        }
    }
}
