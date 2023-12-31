﻿using Moq;
using RentalCompany.Core.Messages.Commands;
using RentalCompany.Domain.Entities;
using RentalCompany.Domain.Handler;
using RentalCompany.Domain.Repositories;
using RentalCompany.Domain.Tests.Commands;
using RentalCompany.Domain.ValueObjects;
using System.Threading.Tasks;
using Xunit;

namespace RentalCompany.Domain.Tests.Handlers
{
    [Collection(nameof(CreateCollectionItemCollection))]
    public class LendCollectionItemCommandHandlerTests
    {
        private readonly CollectionItemCommandTestsFixture _fixture;
        private readonly Mock<ICollectionItemRepository> _collectionItemRepository;
        private readonly Mock<IBorrowerRepository> _borrowerRepositoryMock;
        private readonly Mock<ILocationRepository> _locationRepositoryMock;

        public LendCollectionItemCommandHandlerTests(CollectionItemCommandTestsFixture fixture)
        {
            _fixture = fixture;

            _collectionItemRepository = new Mock<ICollectionItemRepository>();
            _collectionItemRepository.Setup(x => x.UnitOfWork.Commit(default))
                .ReturnsAsync(true);

            _borrowerRepositoryMock = new Mock<IBorrowerRepository>();
            _borrowerRepositoryMock.Setup(x => x.UnitOfWork.Commit(default))
                .ReturnsAsync(true);

            _locationRepositoryMock = new Mock<ILocationRepository>();
            _locationRepositoryMock.Setup(x => x.UnitOfWork.Commit(default))
               .ReturnsAsync(true);
        }

        [Fact]
        public async Task LendCollectionItemCommandHandler_CommandValid_CreateWithSuccess()
        {
            // Arrange
            var command = _fixture.GenerateLendCollectionItemCommandValid();

            _collectionItemRepository.Setup(c => c.GetByIdAsync(command.CollectionItemId))
                .ReturnsAsync(GenericCollectionItem());

            _borrowerRepositoryMock.Setup(c => c.GetByIdAsync(command.BorrowerId!))
                .ReturnsAsync(GenericBorrower());

            var handler = new LendCollectionItemCommandHandler(_collectionItemRepository.Object, _borrowerRepositoryMock.Object);
            // Act
            var result = (CommandResult<CollectionItem>)await handler.HandleAsync(command);

            //Assert
            Assert.True(result.IsSuccess);
            _collectionItemRepository.Verify(r => r.Update(It.IsAny<CollectionItem>()), Times.Once);
            _collectionItemRepository.Verify(r => r.UnitOfWork.Commit(default), Times.Once);
        }

        private static CollectionItem GenericCollectionItem(int quantity = 1)
        {
            return new CollectionItem("Livro Teste", "John Doe", quantity, "Deluxe", EType.BOOK);
        }

        [Fact]
        public async Task LendCollectionItemCommandHandler_CommandInvalid_ShouldReturnError()
        {
            // Arrange
            var command = _fixture.GenerateLendCollectionItemCommandInvalid();

            _collectionItemRepository.Setup(c => c.GetByIdAsync(command.CollectionItemId))
                .ReturnsAsync(GenericCollectionItem());

            _borrowerRepositoryMock.Setup(c => c.GetByIdAsync(command.BorrowerId!))
                .ReturnsAsync(GenericBorrower());

            var handler = new LendCollectionItemCommandHandler(_collectionItemRepository.Object, _borrowerRepositoryMock.Object);
            // Act
            var result = (CommandResult<CollectionItem>)await handler.HandleAsync(command);

            //Assert
            Assert.False(result.IsSuccess);
            _collectionItemRepository.Verify(r => r.Update(It.IsAny<CollectionItem>()), Times.Never);
            _collectionItemRepository.Verify(r => r.UnitOfWork.Commit(default), Times.Never);
        }

        [Fact]
        public async Task LendCollectionItemCommandHandler_CollectionItemIdInvalid_ShouldReturnErrorS()
        {
            // Arrange
            var command = _fixture.GenerateLendCollectionItemCommandValid();
            _collectionItemRepository.Setup(c => c.UnitOfWork.Commit(default))
                .ReturnsAsync(true);
            _collectionItemRepository.Setup(c => c.GetByIdAsync(command.CollectionItemId))
                .ReturnsAsync(null as CollectionItem);

            var handler = new LendCollectionItemCommandHandler(_collectionItemRepository.Object, _borrowerRepositoryMock.Object);
            // Act
            var result = (CommandResult<CollectionItem>)await handler.HandleAsync(command);

            //Assert
            Assert.False(result.IsSuccess);
            _collectionItemRepository.Verify(r => r.Update(It.IsAny<CollectionItem>()), Times.Never);
            _collectionItemRepository.Verify(r => r.UnitOfWork.Commit(default), Times.Never);
        }

        [Fact]
        public async Task LendCollectionItemCommandHandler_CollectionItemUnavailable_ShouldReturnErrorS()
        {
            // Arrange
            var command = _fixture.GenerateLendCollectionItemCommandValid();

            _collectionItemRepository.Setup(c => c.UnitOfWork.Commit(default))
                .ReturnsAsync(true);
            _collectionItemRepository.Setup(c => c.GetByIdAsync(command.CollectionItemId))
                .ReturnsAsync(GenericCollectionItem(0));

            var handler = new LendCollectionItemCommandHandler(_collectionItemRepository.Object, _borrowerRepositoryMock.Object);
            // Act
            var result = (CommandResult<CollectionItem>)await handler.HandleAsync(command);

            //Assert
            Assert.False(result.IsSuccess);
            _collectionItemRepository.Verify(r => r.Update(It.IsAny<CollectionItem>()), Times.Never);
            _collectionItemRepository.Verify(r => r.UnitOfWork.Commit(default), Times.Never);
        }

        [Fact]
        public async Task LendCollectionItemCommandHandler_ContactInvalid_ShouldReturnErrorS()
        {
            // Arrange
            var command = _fixture.GenerateLendCollectionItemCommandValid();

            _collectionItemRepository.Setup(c => c.GetByIdAsync(command.CollectionItemId))
                .ReturnsAsync(GenericCollectionItem());

            _borrowerRepositoryMock.Setup(c => c.GetByIdAsync(command.BorrowerId!))
                .ReturnsAsync(null as Borrower);

            var handler = new LendCollectionItemCommandHandler(_collectionItemRepository.Object, _borrowerRepositoryMock.Object);
            // Act
            var result = (CommandResult<CollectionItem>)await handler.HandleAsync(command);
            //Assert
            Assert.False(result.IsSuccess);
            _collectionItemRepository.Verify(r => r.Update(It.IsAny<CollectionItem>()), Times.Never);
            _collectionItemRepository.Verify(r => r.UnitOfWork.Commit(default), Times.Never);
        }

        [Fact]
        public async Task LendCollectionItemCommandHandler_ContactIdNull_ShouldReturnErrors()
        {
            // Arrange
            var command = _fixture.GenerateLendCollectionItemCommandWithoutContactIdValid();

            _collectionItemRepository.Setup(c => c.GetByIdAsync(command.CollectionItemId))
                .ReturnsAsync(GenericCollectionItem());

            _borrowerRepositoryMock.Setup(c => c.GetByIdAsync(command.BorrowerId!))
                .ReturnsAsync(GenericBorrower());

            var handler = new LendCollectionItemCommandHandler(_collectionItemRepository.Object, _borrowerRepositoryMock.Object);
            // Act
            var result = (CommandResult<CollectionItem>)await handler.HandleAsync(command);

            //Assert
            Assert.False(result.IsSuccess);
            _collectionItemRepository.Verify(r => r.Update(It.IsAny<CollectionItem>()), Times.Never);
            _collectionItemRepository.Verify(r => r.UnitOfWork.Commit(default), Times.Never);
        }

        private static Borrower GenericBorrower()
        {
            return new Borrower("Maria Doe", "maria@mail.com", Email.Create("johndoe@mail.com"),
                "", new Address("Rua tal", "9846000", "Los Angeles", "312"));
        }

    }
}