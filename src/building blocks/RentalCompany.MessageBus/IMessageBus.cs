using RentalCompany.Core.Contracts;

namespace RentalCompany.MessageBus
{
    public interface IMessageBus
    {
        void Publish<T>(T message) where T : IIntegrationEvent;
    }
}
