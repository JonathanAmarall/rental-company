using RentalCompany.Core.Messages.Commands;

namespace RentalCompany.Core.Contracts
{
    public interface IHandlerAsync<T> where T : ICommand
    {
        Task<ICommandResult> HandleAsync(T command);
    }
}
