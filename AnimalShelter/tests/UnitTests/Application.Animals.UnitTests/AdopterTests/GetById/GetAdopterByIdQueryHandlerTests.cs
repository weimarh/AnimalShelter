using Application.Adopters.GetById;
using Domain.Adopters;
using Domain.ValueObjects;

namespace Application.Animals.UnitTests.AdopterTests.GetById;

[TestClass]
public class GetAdopterByIdQueryHandlerTests
{
    [TestMethod]
    public async Task HandleGetAdopterById_WhenAdopterExists_ShouldReturnAdopter()
    {
        // Arrange
        var mockAdopterRepository = new Mock<IAdopterRepository>();
        var handler = new GetAdopterByIdQueryHandler(mockAdopterRepository.Object);

        var adopterId = Guid.NewGuid();
        var adopter = new Adopter(
            new AdopterId(adopterId),
            "test",
            "test",
            PhoneNumber.Create("70792462"),
            Address.Create(
                "test", "test", "test", 1313),
            "wba@homai.com"
        );

        var command = new GetAdopterByIdQuery(adopterId);

        mockAdopterRepository.Setup(repo => repo.GetByIdAsync(new AdopterId(command.Id)))
            .ReturnsAsync(adopter);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.AreEqual(result.IsError, false);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(result.Value.Id, adopter.Id.Id);
    }

    [TestMethod]
    public async Task HandleGetAdopterById_WhenAdopterDoesNotExist_ShouldReturnAdopterNotFound()
    {
        // Arrange
        var mockAdopterRepository = new Mock<IAdopterRepository>();
        var handler = new GetAdopterByIdQueryHandler(mockAdopterRepository.Object);

        var adopterId = Guid.NewGuid();
        var command = new GetAdopterByIdQuery(adopterId);

        mockAdopterRepository.Setup(repo => repo.GetByIdAsync(new AdopterId(command.Id)))
            .ReturnsAsync((Adopter?)null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().Contain(Errors.Adopter.AdopterNotFound);
    }
}
