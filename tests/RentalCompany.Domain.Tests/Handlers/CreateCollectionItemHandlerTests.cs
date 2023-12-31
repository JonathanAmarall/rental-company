﻿using Moq;
using RentalCompany.Core.Messages.Commands;
using RentalCompany.Domain.Entities;
using RentalCompany.Domain.Handler;
using RentalCompany.Domain.Repositories;
using RentalCompany.Domain.Tests.Commands;
using System.Threading.Tasks;
using Xunit;

namespace RentalCompany.Domain.Tests.Handlers
{
    [Collection(nameof(CreateCollectionItemCollection))]
    public class CreateCollectionItemHandlerTests
    {
        private readonly CollectionItemCommandTestsFixture _fixture;

        public CreateCollectionItemHandlerTests(CollectionItemCommandTestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task CollectionItemHandler_CreateCollectionItemCommand_CreateWithSuccess()
        {
            // Arrange
            var collectionItemRepository = new Mock<ICollectionItemRepository>();
            collectionItemRepository.Setup(c => c.UnitOfWork.Commit(default)).ReturnsAsync(true);

            var command = _fixture.GenerateCreateCollectionItemCommandValid();
            var handler = new CreateCollectionItemCommandHandler(collectionItemRepository.Object);

            // Act
            var result = (CommandResult<CollectionItem>)await handler.HandleAsync(command);

            // Assert
            Assert.True(result.IsSuccess);
            collectionItemRepository.Verify(r => r.CreateAsync(It.IsAny<CollectionItem>()), Times.Once);
            collectionItemRepository.Verify(r => r.UnitOfWork.Commit(default), Times.Once);
        }

        [Fact]
        public async Task CollectionItemHandler_CreateCollectionItemCommand_CreateFail()
        {
            // Arrange
            var collectionItemRepository = new Mock<ICollectionItemRepository>();
            collectionItemRepository.Setup(c => c.UnitOfWork.Commit(default)).ReturnsAsync(false);

            var command = _fixture.GenerateCreateCollectionItemCommandInvalid();
            var handler = new CreateCollectionItemCommandHandler(collectionItemRepository.Object);

            // Act
            var result = (CommandResult<CollectionItem>)await handler.HandleAsync(command);

            // Assert
            Assert.False(result.IsSuccess);
            collectionItemRepository.Verify(r => r.CreateAsync(It.IsAny<CollectionItem>()), Times.Never);
            collectionItemRepository.Verify(r => r.UnitOfWork.Commit(default), Times.Never);
        }
    }
}