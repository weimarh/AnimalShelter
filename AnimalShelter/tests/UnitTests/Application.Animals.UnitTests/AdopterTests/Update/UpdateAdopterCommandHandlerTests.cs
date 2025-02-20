using Application.Adopters.Update;
using Domain.Adopters;
using Domain.Primitives;
using Domain.ValueObjects;

namespace Application.Animals.UnitTests.AdopterTests.Update;

[TestClass]
public class UpdateAdopterCommandHandlerTests
{
    [TestMethod]
    public async Task Handle_AdopterExists_UpdatesAdopterAndReturnsUnit()
    {
        // Arrange
        var mockAdopterRepository = new Mock<IAdopterRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var handler = new UpdateAdopterCommandHandler(mockAdopterRepository.Object, mockUnitOfWork.Object);

        var adopterId = Guid.NewGuid();
        var command = new UpdateAdopterCommand(
            adopterId,
            "UpdatedFirstName",
            "UpdatedLastName",
            "70712345",
            "UpdatedCountry",
            "UpdatedCity",
            "UpdatedStreet",
            12345,
            "UpdatedEmail");

        var adopter = new Adopter(
            new AdopterId(adopterId),
            "FirstName",
            "LastName",
            PhoneNumber.Create("70795462"),
            Address.Create(
                "test", "test", "test", 1313),
            "wba@homai.com");

        mockAdopterRepository.Setup(repo => repo.ExistsAsync(new AdopterId(command.Id)))
                .ReturnsAsync(true);
        mockAdopterRepository.Setup(repo => repo.GetByIdAsync(new AdopterId(command.Id)))
                .ReturnsAsync(adopter);
        mockUnitOfWork.Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsError.Should().BeFalse();
        mockAdopterRepository.Verify(repo => repo.Update(It.IsAny<Adopter>()), Times.Once);
        mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task HandleUpdateAdopter_WhenAdopterDoesNotExist_ShouldThrowAdopterNotFoundException()
    {
        var mockAdopterRepository = new Mock<IAdopterRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var handler = new UpdateAdopterCommandHandler(mockAdopterRepository.Object, mockUnitOfWork.Object);

        var adopterId = Guid.NewGuid();
        var command = new UpdateAdopterCommand(
            adopterId,
            "UpdatedFirstName",
            "UpdatedLastName",
            "70712345",
            "UpdatedCountry",
            "UpdatedCity",
            "UpdatedStreet",
            12345,
            "UpdatedEmail");

        mockAdopterRepository.Setup(repo => repo.GetByIdAsync(new AdopterId(command.Id)))
            .ReturnsAsync((Adopter?)null);
        // Act 
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().Contain(Errors.Adopter.AdopterNotFound);
        mockAdopterRepository.Verify(repo => repo.Remove(It.IsAny<Adopter>()), Times.Never);
            mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), 
            Times.Never);
    }
}
