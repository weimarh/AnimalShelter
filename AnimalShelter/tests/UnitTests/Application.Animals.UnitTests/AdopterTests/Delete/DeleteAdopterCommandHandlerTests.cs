using Application.Adopters.Delete;
using Domain.Adopters;
using Domain.Primitives;
using Domain.ValueObjects;

namespace Application.Animals.UnitTests.AdopterTests.Delete;

[TestClass]
public class DeleteAdopterCommandHandlerTests
{
    [TestMethod]
    public async Task Handle_DeleteAdopterCommand_ReturnsSuccess()
    {
        // Arrange
        var mockAdopterRepository = new Mock<IAdopterRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var handler = new DeleteAdopterCommandHandler(
            mockAdopterRepository.Object, mockUnitOfWork.Object);
        
        var AdopterId = Guid.NewGuid();
        var adopter = new Adopter(
            new AdopterId(AdopterId),
            "test",
            "test",
            PhoneNumber.Create("70792462"),
            Address.Create(
                "test", "test", "test", 1313),
            "wba@homai.com"
        );
        var command = new DeleteAdopterCommand(AdopterId);


        mockAdopterRepository.Setup(repo => repo.GetByIdAsync(new AdopterId(command.Id)))
            .ReturnsAsync(adopter);

        mockUnitOfWork.Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().Be(Unit.Value);

        mockAdopterRepository.Verify(repo => repo.Remove(adopter), Times.Once);
        mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task HandleDeleteAnimal_WhenAnimalDoesNotExist_ShouldThrowAnimalNotFoundException()
    {
        // Arrange
        var mockAdopterRepository = new Mock<IAdopterRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var handler = new DeleteAdopterCommandHandler(
            mockAdopterRepository.Object, mockUnitOfWork.Object);

        DeleteAdopterCommand command = new DeleteAdopterCommand(Guid.NewGuid());

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
