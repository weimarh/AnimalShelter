using Application.Adopters.GetAll;
using Domain.Adopters;
using Domain.ValueObjects;

namespace Application.Animals.UnitTests.AdopterTests.GetAll;

[TestClass]
public class GetAllAdoptersQueryHandlerTests
{
    [TestMethod]
    public async Task HandleGetAllAdopters_ReturnsAllAdopters()
    {
        // Arrange
        var mockAdopterRepository = new Mock<IAdopterRepository>();
        var handler = new GetAllAdoptersQueryHandler(mockAdopterRepository.Object);

        var adopters = new List<Adopter>
            {
                new Adopter
                (
                    new AdopterId(Guid.NewGuid()),
                    "John",
                    "Doe",
                    PhoneNumber.Create("70792462"),
                    Address.Create("USA", "New York", "1st St", 1),
                    "john.doe@example.com"
                ),
                new Adopter
                (
                    new AdopterId(Guid.NewGuid()),
                    "Jane",
                    "Smith",
                    PhoneNumber.Create("79612345"),
                    Address.Create("Canada", "Toronto", "2nd St", 10),
                    "jane.smith@example.com"
                )
            };

        mockAdopterRepository.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(adopters);
        
        // Act
        var result = await handler.Handle(new GetAllAdoptersQuery(), CancellationToken.None);

        // Assert
        Assert.AreEqual(result.IsError, false);
        Assert.AreEqual(result.Value.Count, 2);
    }
}
