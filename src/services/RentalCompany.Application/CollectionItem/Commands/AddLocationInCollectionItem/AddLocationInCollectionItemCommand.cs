using FluentValidation.Results;
using RentalCompany.Core.Contracts;
using RentalCompany.Core.Messages.Commands;
using System.Text.Json.Serialization;

namespace RentalCompany.Domain.Commands
{
    public class AddLocationInCollectionItemCommand : ICommand
    {

        [JsonIgnore]
        public ValidationResult? ValidationResult { get; set; }
        public AddLocationInCollectionItemCommand(Guid collectionItemId, Guid locationId)
        {
            CollectionItemId = collectionItemId;
            LocationId = locationId;
        }


        public Guid CollectionItemId { get; set; }
        public Guid LocationId { get; set; }

        public bool IsValid()
        {
            ValidationResult = new AddLocationInCollectionItemCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
