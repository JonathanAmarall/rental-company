using MediatR;

namespace RentalCompany.Core.Contracts;

public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
        where TDomainEvent : IDomainEvent
{
}
