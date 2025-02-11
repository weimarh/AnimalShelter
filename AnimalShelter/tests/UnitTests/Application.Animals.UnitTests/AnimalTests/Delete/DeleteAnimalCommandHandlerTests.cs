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
        // Act 
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.IsError.Should().BeTrue();
    }
}
