using Application.Animals.GetAll;
using Domain.Animals;

namespace Application.Animals.UnitTests.AnimalTests.GetAll;

[TestClass]
public class GetAllAnimalsQueryHandlerTests
{
    [TestMethod]
    public async Task HandleGetAllAnimals_ReturnsAllAnimals()
    {
        // Arrange
        var mockAnimalRepository = new Mock<IAnimalRepository>();
        var handler = new GetAllAnimalsQueryHandler(mockAnimalRepository.Object);

        var animals = new List<Animal>
        {
            new Animal(
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
                "test"),
            new Animal(
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
                "test")
        };

        mockAnimalRepository.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(animals);

        // Act
        var result = await handler.Handle(new GetAllAnimalsQuery(), CancellationToken.None);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().HaveCount(2);
    }

    [TestMethod]
    public async Task HandleGetAllAnimals_WhenNoAnimalsExist_ReturnsEmptyList()
    {
        // Arrange
        var mockAnimalRepository = new Mock<IAnimalRepository>();
        var handler = new GetAllAnimalsQueryHandler(mockAnimalRepository.Object);

        mockAnimalRepository.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(new List<Animal>());

        // Act
        var result = await handler.Handle(new GetAllAnimalsQuery(), CancellationToken.None);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().BeEmpty();
    }
}
