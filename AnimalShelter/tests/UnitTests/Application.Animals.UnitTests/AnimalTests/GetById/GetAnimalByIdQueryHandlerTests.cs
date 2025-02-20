using Application.Animals.GetById;
using Domain.Animals;

namespace Application.Animals.UnitTests.AnimalTests.GetById;

[TestClass]
public class GetAnimalByIdQueryHandlerTests
{
    [TestMethod]
    public async Task HandleGetAnimalById_WhenAnimalDoesNotExist_ShouldReturnAnimalNotFound()
    {
        // Arrange
        var mockAnimalRepository = new Mock<IAnimalRepository>();
        var handler = new GetAnimalByIdQueryHandler(mockAnimalRepository.Object);

        var animalId = Guid.NewGuid();
        var command = new GetAnimalByIdQuery(animalId);

        mockAnimalRepository.Setup(repo => repo.GetByIdAsync(new AnimalId(command.Id)))
            .ReturnsAsync((Animal?)null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().Contain(Errors.Animal.AnimalNotFound);
    }

    [TestMethod]
    public async Task HandleGetAnimalById_WhenAnimalExists_ShouldReturnAnimal()
    {
        // Arrange
        var mockAnimalRepository = new Mock<IAnimalRepository>();
        var handler = new GetAnimalByIdQueryHandler(mockAnimalRepository.Object);

        var animalId = Guid.NewGuid();
        var animal = new Animal(
            new AnimalId(Guid.NewGuid()),
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
            
        var command = new GetAnimalByIdQuery(animal.Id.Id);

        mockAnimalRepository.Setup(repo => repo.GetByIdAsync(new AnimalId(command.Id)))
            .ReturnsAsync(animal);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsError.Should().BeFalse();
    }
}
