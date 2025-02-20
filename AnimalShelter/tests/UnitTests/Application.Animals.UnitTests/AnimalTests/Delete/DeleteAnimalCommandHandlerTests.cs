using Application.Animals.Delete;
using Domain.Animals;
using Domain.Primitives;

namespace Application.Animals.UnitTests.AnimalTests.Delete;

[TestClass]
public class DeleteAnimalCommandHandlerTests
{
    [TestMethod]
    public async Task HandleDeleteAnimal_WhenAnimalDoesNotExist_ShouldThrowAnimalNotFoundException()
    {
        // Arrange
        var mockAnimalRepository = new Mock<IAnimalRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var handler = new DeleteAnimalCommandHandler(
            mockAnimalRepository.Object, mockUnitOfWork.Object);

        DeleteAnimalCommand command = new DeleteAnimalCommand(Guid.NewGuid());

        mockAnimalRepository.Setup(repo => repo.GetByIdAsync(new AnimalId(command.Id)))
            .ReturnsAsync((Animal?)null);
        // Act 
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().Contain(Errors.Animal.AnimalNotFound);
        mockAnimalRepository.Verify(repo => repo.Remove(It.IsAny<Animal>()), Times.Never);
            mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), 
            Times.Never);
    }

    [TestMethod]
    public async Task Handle_AnimalExists_RemovesAnimalAndReturnsUnit()
    {
        // Arrange
        var mockAnimalRepository = new Mock<IAnimalRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var handler = new DeleteAnimalCommandHandler(mockAnimalRepository.Object, mockUnitOfWork.Object);

        var animalId = Guid.NewGuid();
        var animal = new Animal(
            new AnimalId(animalId),
            "test",
            "test",
            "test",
            "test",
            "test",
            "test",
            DateTimeOffset.Now,
            "test",
            "test",
            "test");
        var command = new DeleteAnimalCommand(animalId);

        mockAnimalRepository.Setup(repo => repo.GetByIdAsync(new AnimalId(command.Id)))
            .ReturnsAsync(animal);

        mockUnitOfWork.Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsError.Should().BeFalse(); // Fluent Assertion
        result.Value.Should().Be(Unit.Value); // Fluent Assertion

        mockAnimalRepository.Verify(repo => repo.Remove(animal), Times.Once);
        mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
