using FluentValidation;

namespace RentalCompany.Domain.Commands
{
    public class LendCollectionItemCommandValidation : AbstractValidator<RentItemCommand>
    {
        public LendCollectionItemCommandValidation()
        {
            RuleFor(c => c.CollectionItemId)
                .NotEqual(Guid.Empty)
                .WithMessage("Informe um Item corretamente.");
            RuleFor(c => c.BorrowerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Informe um Locatario corretamente.");

            RuleFor(c => c.RentQuantity)
                .GreaterThan(0);
        }
    }
}