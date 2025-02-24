using Application.Adoptions.Update;
using Domain.Adopters;
using Domain.Adoptions;
using Domain.Animals;
using Domain.Primitives;
using Domain.ValueObjects;

namespace Application.Animals.UnitTests.AdoptionTests.Update;

[TestClass]
public class UpdateAdoptionCommandHandlerTests
{
    [TestMethod]
    public async Task HandleUpdateAdoption_WhenAnimalIdIsEmpty_ShouldReturnValidationError()
    {
        var mockAdoptionRepository = new Mock<IAdoptionRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockAnimalRepository = new Mock<IAnimalRepository>();
        var mockAdopterRepository = new Mock<IAdopterRepository>();
        var handler = new UpdateAdoptionCommandHandler(
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

        var adoption = new Adoption(
            new AdoptionId(new Guid()),
            animal.Id,
            adopter.Id,
            new DateTimeOffset());

        UpdateAdoptionCommand command = new UpdateAdoptionCommand(
            adoption.Id.Id,
            "",
            adopter.Id.ToString(),
            new DateTimeOffset());
        
        mockAdoptionRepository.Setup(repo => repo.ExistsAsync(new AdoptionId(command.Id)))
            .ReturnsAsync(true);
        
        //Act
        var result = await handler.Handle(command, default);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Code.Should().Be(Errors.Adoption.AnimalIdDoesNotExist.Code);
        result.FirstError.Description.Should().Be(Errors.Adoption.EmptyAnimalId.Description);
    }

    [TestMethod]
    public async Task HandleUpdateAdoption_WhenAdopterIdIsEmpty_ShouldReturnValidationError()
    {
        var mockAdoptionRepository = new Mock<IAdoptionRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockAnimalRepository = new Mock<IAnimalRepository>();
        var mockAdopterRepository = new Mock<IAdopterRepository>();
        var handler = new UpdateAdoptionCommandHandler(
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

        var adoption = new Adoption(
            new AdoptionId(new Guid()),
            animal.Id,
            adopter.Id,
            new DateTimeOffset());

        UpdateAdoptionCommand command = new UpdateAdoptionCommand(
            adoption.Id.Id,
            animal.Id.ToString(),
            "",
            new DateTimeOffset());
        
        mockAdoptionRepository.Setup(repo => repo.ExistsAsync(new AdoptionId(command.Id)))
            .ReturnsAsync(true);
        
        //Act
        var result = await handler.Handle(command, default);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Code.Should().Be(Errors.Adoption.AdopterIdDoesNotExist.Code);
        result.FirstError.Description.Should().Be(Errors.Adoption.EmptyAdopterId.Description);
    }
}
