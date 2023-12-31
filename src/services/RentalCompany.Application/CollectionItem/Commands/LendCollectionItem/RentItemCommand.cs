﻿using FluentValidation.Results;
using RentalCompany.Core.Messages.Commands;
using System.Text.Json.Serialization;

namespace RentalCompany.Domain.Commands
{
    public class RentItemCommand : ICommand
    {
        [JsonIgnore]
        public ValidationResult? ValidationResult { get; set; }

        public RentItemCommand(Guid collectionItemId, Guid borrowerId, int rentQuantity)
        {
            CollectionItemId = collectionItemId;
            BorrowerId = borrowerId;
            RentQuantity = rentQuantity;
        }

        public Guid CollectionItemId { get; private set; }
        public Guid BorrowerId { get; private set; }
        public int RentQuantity { get; private set; }

        public bool IsValid()
        {
            ValidationResult = new LendCollectionItemCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}