using Application.Adoptions.GetAll;
using Domain.Adopters;
using Domain.Adoptions;
using Domain.Animals;

namespace Application.Animals.UnitTests.AdoptionTests.GetAll;

[TestClass]
public class GetAllAdoptionsQueryHandlerTests
{
    [TestMethod]
    public async Task GetAllAdoptions_ReturnAllAdopters()
    {
        // Arrange
        var mockAdoptionRepository = new Mock<IAdoptionRepository>();
        var handler = new GetAllAdoptionsQueryHandler(mockAdoptionRepository.Object);

        var adoptions = new List<Adoption>
        {
            new Adoption
            (
                new AdoptionId(Guid.NewGuid()),
                new AnimalId(Guid.NewGuid()),
                new AdopterId(Guid.NewGuid()),
                DateTimeOffset.Now
            ),
            new Adoption
            (
                new AdoptionId(Guid.NewGuid()),
                new AnimalId(Guid.NewGuid()),
                new AdopterId(Guid.NewGuid()),
                DateTimeOffset.Now
            )
        };

        mockAdoptionRepository.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(adoptions);
        
        // Act
        var result = await handler.Handle(new GetAllAdoptionsQuery(), CancellationToken.None);

        // Assert
        Assert.AreEqual(result.IsError, false);
        Assert.AreEqual(result.Value.Count, 2);
    }
}
