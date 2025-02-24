using Application.Adoptions.GetById;
using Domain.Adopters;
using Domain.Adoptions;
using Domain.Animals;

namespace Application.Animals.UnitTests.AdoptionTests.GetById;

[TestClass]
public class GetAdoptionByIdQueryHandlerTests
{
    [TestMethod]
    public async Task HandleGetAdoptionById_WhenAdopterExists_ShouldReturnAdoption()
    {
        //Arrange
        var mockAdoptionRepository = new Mock<IAdoptionRepository>();
        var handler = new GetAdoptionByIdQueryHandler(mockAdoptionRepository.Object);

        var adoption = new Adoption
        (
            new AdoptionId(Guid.NewGuid()),
            new AnimalId(Guid.NewGuid()),
            new AdopterId(Guid.NewGuid()),
            DateTimeOffset.Now
        );

        var command = new GetAdoptionByIdQuery(adoption.Id.Id);

        mockAdoptionRepository.Setup(repo => repo.GetByIdAsync(adoption.Id))
            .ReturnsAsync(adoption);
        
        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        Assert.AreEqual(result.IsError, false);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(result.Value.Id, adoption.Id.Id);
    }

    [TestMethod]
    public async Task HandleGetAdoptionById_WhenAdopterDoestExists_ShouldReturnotFound()
    {
        //Arrange
        var mockAdoptionRepository = new Mock<IAdoptionRepository>();
        var handler = new GetAdoptionByIdQueryHandler(mockAdoptionRepository.Object);

        var adoptionId = Guid.NewGuid();
        var command = new GetAdoptionByIdQuery(adoptionId);

        mockAdoptionRepository.Setup(repo => repo.GetByIdAsync(new AdoptionId(command.Id)))
            .ReturnsAsync((Adoption?)null);
        
        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().Contain(Errors.Adoption.AdoptionNotFound);
    }
}
