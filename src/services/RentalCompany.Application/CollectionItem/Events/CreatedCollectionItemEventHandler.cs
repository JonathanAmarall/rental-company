using RentalCompany.Core.Contracts;
using RentalCompany.Domain.Events;

namespace RentalCompany.Application.CollectionItem.Events;

public class CreatedCollectionItemEventHandler : IDomainEventHandler<CreatedCollectionItemDomainEvent>
{
    public async Task Handle(CreatedCollectionItemDomainEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"O Item {notification.Item.Title} foi cadastrado.");

        await Task.CompletedTask;
    }

}
