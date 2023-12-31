﻿using FluentAssertions;
using Moq;
using RentalCompany.Core.Messages.Commands;
using RentalCompany.Domain.Commands;
using RentalCompany.Domain.Entities;
using RentalCompany.Domain.Handler;
using RentalCompany.Domain.Repositories;
using System;
using System.Threading.Tasks;
using Xunit;

namespace RentalCompany.Domain.Tests.Handlers
{
    public class CreateLocationCommandHandlerTests
    {
        private readonly Mock<ILocationRepository> _locationRepositoryMock;
        public CreateLocationCommandHandlerTests()
        {
            _locationRepositoryMock = new Mock<ILocationRepository>();
            _locationRepositoryMock.Setup(c => c.UnitOfWork.Commit(default))
                .ReturnsAsync(true);
        }

        [Fact]
        public async Task CreateLocationCommandHandler_WithValidCommand_ShouldCreateLocation()
        {
            // Assert
            var validCommand = new CreateLocationCommand("CXA", "Caixa", null);
            var handler = new CreateLocationCommandHandler(_locationRepositoryMock.Object);
            // Act
            var result = (CommandResult<Location>)await handler.HandleAsync(validCommand);
            // Arrange
            result.IsSuccess.Should().BeTrue();
            result.ValidationResult.Should().BeNull();
        }

        [Fact]
        public async Task CreateLocationCommandHandler_WithValidCommandAndWithParent_ShouldCreateLocation()
        {
            // Assert
            const int nextLevel = 1;
            var validCommand = new CreateLocationCommand("CXA", "Caixa", Guid.NewGuid());
            var handler = new CreateLocationCommandHandler(_locationRepositoryMock.Object);
            var parentLocation = new Location("PRT", "Prateleira", null, 1);

            _locationRepositoryMock.Setup(x => x.GetByIdAsync(validCommand.ParentId!.Value))
                .ReturnsAsync(parentLocation);
            // Act
            var result = (CommandResult<Location>)await handler.HandleAsync(validCommand);
            // Arrange
            var resultDataLocation = result.Data;
            result.IsSuccess.Should().BeTrue();
            result.ValidationResult.Should().BeNull();
            resultDataLocation!.Level.Should().Be(parentLocation.Level + nextLevel);
        }

        [Fact]
        public async Task CreateLocationCommandHandler_WithInvalidCommand_ShouldCreateLocation()
        {
            // Assert
            var validCommand = new CreateLocationCommand(string.Empty, string.Empty, null);
            var handler = new CreateLocationCommandHandler(_locationRepositoryMock.Object);
            // Act
            var result = (CommandResult<Location>)await handler.HandleAsync(validCommand);
            // Arrange
            result.IsSuccess.Should().BeFalse();
            result.ValidationResult.Should().NotBeNull();
        }
    }
}
