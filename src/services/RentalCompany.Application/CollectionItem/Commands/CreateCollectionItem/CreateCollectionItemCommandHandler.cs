using RentalCompany.Core.Contracts;
using RentalCompany.Core.Messages.Commands;
using RentalCompany.Domain.Entities;
using RentalCompany.Domain.Repositories;

namespace RentalCompany.Domain.Handler
{
    public class CreateCollectionItemCommandHandler : IHandlerAsync<CreateCollectionItemCommand>
    {
        private readonly ICollectionItemRepository _collectionItemRepository;

        public CreateCollectionItemCommandHandler(ICollectionItemRepository collectionItemRepository)
        {
            _collectionItemRepository = collectionItemRepository;
        }

        public async Task<ICommandResult> HandleAsync(CreateCollectionItemCommand command)
        {
            if (!command.IsValid())
            {
                return CommandResult<CollectionItem>.Failure("Ops, parece que há algo de errado.", command.ValidationResult);
            }

            var item = new CollectionItem(command.Title, command.Autor, command.Quantity, command.Edition, (EType)command.ItemType);

            await _collectionItemRepository.CreateAsync(item);
            await _collectionItemRepository.UnitOfWork.Commit();

            return CommandResult<CollectionItem>.Success("Cadastrado com sucesso!", item);
        }
    }
}