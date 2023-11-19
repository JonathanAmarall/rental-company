using RentalCompany.Core.Contracts;
using RentalCompany.Domain.Entities;

namespace RentalCompany.Domain.Events
{
    public class CreatedCollectionItemDomainEvent : IDomainEvent
    {
        public CreatedCollectionItemDomainEvent(CollectionItem item)
        {
            Item = item;
        }

        public CollectionItem Item { get; private set; }
    }
}