using RentalCompany.Core.Contracts;
using RentalCompany.Core.Messages.Commands;
using RentalCompany.Domain.Repositories;
using RentalCompany.Domain.ValueObjects;

namespace RentalCompany.Application.Borrower.Commands.CreateBorrower
{
    public class CreateBorrowerCommandHandler : IHandlerAsync<CreateBorrowerCommand>
    {
        private readonly IBorrowerRepository _borrowerRepository;

        public CreateBorrowerCommandHandler(IBorrowerRepository borrowerRepository)
        {
            _borrowerRepository = borrowerRepository;
        }

        public async Task<ICommandResult> HandleAsync(CreateBorrowerCommand command)
        {
            if (!command.IsValid())
            {
                return CommandResult<Domain.Entities.Borrower>.Failure("Ops, parece que há erros de validação.", command.ValidationResult);
            }

            var borrower = new Domain.Entities.Borrower(
                command.FirstName,
                command.LastName,
                Email.Create(command.Email),
                command.Phone,
                Address.Create(command.Street, command.PostalCode, command.City, command.Number));

            await _borrowerRepository.CreateBorrowerAsync(borrower);
            await _borrowerRepository.UnitOfWork.Commit();

            return CommandResult<Domain.Entities.Borrower>.Success("Borrower created with success", borrower);
        }
    }
}
