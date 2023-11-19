using RentalCompany.Core.Contracts;
using RentalCompany.Domain.Entities;
using RentalCompany.Domain.Events;
using RentalCompany.Domain.Repositories;
using RentalCompany.MessageBus;

namespace RentalCompany.Application.CollectionItem.Events
{
    public class RentItemDomainEventHandler : IDomainEventHandler<RentItemDomainEvent>
    {
        private readonly IRentItemRepository _rentItemRepository;
        private readonly IMessageBus _bus;
        public RentItemDomainEventHandler(IRentItemRepository rentItemRepository, IMessageBus bus)
        {
            _rentItemRepository = rentItemRepository;
            _bus = bus;
        }

        public async Task Handle(RentItemDomainEvent notification, CancellationToken cancellationToken)
        {
            var rentItem = new RentItem(notification.RentedQuantity, notification.Item.Id, notification.Borrower.Id);
            await _rentItemRepository.CreateAsync(rentItem);

            _bus.Publish(new RentItemIntegrationEvent(notification, rentItem.RentDueDate, notification.Item.Title));

            await Task.CompletedTask;
        }
    }
}