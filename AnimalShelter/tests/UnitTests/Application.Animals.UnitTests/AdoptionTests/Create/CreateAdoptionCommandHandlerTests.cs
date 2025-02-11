using Application.Adoptions.Create;
using Domain.Adopters;
using Domain.Adoptions;
using Domain.Animals;
using Domain.Primitives;
using Domain.ValueObjects;

namespace Application.Animals.UnitTests.AdoptionTests.Create;

[TestClass]
public class CreateAdoptionCommandHandlerTests
{
    [TestMethod]
    public async Task HandleCreateAdoption_WhenAnimalIdIsEmpty_ShouldReturnValidationError()
    {
        var mockAdoptionRepository = new Mock<IAdoptionRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockAnimalRepository = new Mock<IAnimalRepository>();
        var mockAdopterRepository = new Mock<IAdopterRepository>();
        var handler = new CreateAdoptionCommandHandler(
            mockAdoptionRepository.Object,
            mockUnitOfWork.Object,
            mockAdopterRepository.Object,
            mockAnimalRepository.Object);
        
        //Arrange
        var animal = new Animal(
            new AnimalId(new Guid()),
            "Test",
            "Test",
            "Test",
            "Test",
            "Test",
            "Test",
            new DateTimeOffset(),
            "Test",
            "Test",
            "Test");
        
        var adopter = new Adopter(
            new AdopterId(new Guid()),
            "Test",
            "Test",
            PhoneNumber.Create("70792462"),
            Address.Create("Test", "Test", "Test", 179),
            "Test");


        CreateAdoptionCommand command = new CreateAdoptionCommand(
            "",
            adopter.Id.ToString(),
            new DateTimeOffset());
        
        //Act
        var result = await handler.Handle(command, default);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Code.Should().Be(Errors.Adoption.EmptyAnimalId.Code);
        result.FirstError.Description.Should().Be(Errors.Adoption.EmptyAnimalId.Description);
    }

    [TestMethod]
    public async Task HandleCreateAdoption_WhenAdopterIdIsEmpty_ShouldReturnValidationError()
    {
        var mockAdoptionRepository = new Mock<IAdoptionRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockAnimalRepository = new Mock<IAnimalRepository>();
        var mockAdopterRepository = new Mock<IAdopterRepository>();
        var handler = new CreateAdoptionCommandHandler(
            mockAdoptionRepository.Object,
            mockUnitOfWork.Object,
            mockAdopterRepository.Object,
            mockAnimalRepository.Object);
        
        //Arrange
        var animal = new Animal(
            new AnimalId(new Guid()),
            "Test",
            "Test",
            "Test",
            "Test",
            "Test",
            "Test",
            new DateTimeOffset(),
            "Test",
            "Test",
            "Test");
        
        var adopter = new Adopter(
            new AdopterId(new Guid()),
            "Test",
            "Test",
            PhoneNumber.Create("70792462"),
            Address.Create("Test", "Test", "Test", 179),
            "Test");


        CreateAdoptionCommand command = new CreateAdoptionCommand(
            animal.Id.ToString(),
            "",
            new DateTimeOffset());
        
        //Act
        var result = await handler.Handle(command, default);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Code.Should().Be(Errors.Adoption.EmptyAdopterId.Code);
        result.FirstError.Description.Should().Be(Errors.Adoption.EmptyAdopterId.Description);
    }
}
