using Application.Animals.Create;
using Domain.Animals;
using Domain.Primitives;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Application.Animals.UnitTests.AnimalTests.Create;

[TestClass]
public class CreateAnimalCommandHandlerTests
{

    [TestMethod]
    public async Task HandleCreateAnimal_WhenNameIsAnEmptyField_ShouldReturnValidationError()
    {
        var mockAnimalRepository = new Mock<IAnimalRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var handler = new CreateAnimalCommandHandler(
            mockAnimalRepository.Object, mockUnitOfWork.Object);
        //Arrange
        CreateAnimalCommand command = new CreateAnimalCommand(
            "",
            "Test",
            "Test",
            "Test",
            "Test",
            "Test",
            new DateTimeOffset(),
            "Test",
            "Test",
            "Test");

        //Act
        var result = await handler.Handle(command, default);
        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Code.Should().Be(Errors.Animal.EmptyName.Code);
        result.FirstError.Description.Should().Be(Errors.Animal.EmptyName.Description);
    }

    [TestMethod]
    public async Task HandleCreateAnimal_WhenSpeciesIsAnEmptyField_ShouldReturnValidationError()
    {
        var mockAnimalRepository = new Mock<IAnimalRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var handler = new CreateAnimalCommandHandler(
            mockAnimalRepository.Object, mockUnitOfWork.Object);
        //Arrange
        CreateAnimalCommand command = new CreateAnimalCommand(
            "Test",
            "",
            "Test",
            "Test",
            "Test",
            "Test",
            new DateTimeOffset(),
            "Test",
            "Test",
            "Test");

        //Act
        var result = await handler.Handle(command, default);
        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Code.Should().Be(Errors.Animal.EmptySpecies.Code);
        result.FirstError.Description.Should().Be(Errors.Animal.EmptySpecies.Description);
    }

    [TestMethod]
    public async Task HandleCreateAnimal_WhenSexIsAnEmptyField_ShouldReturnValidationError()
    {
        var mockAnimalRepository = new Mock<IAnimalRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var handler = new CreateAnimalCommandHandler(
            mockAnimalRepository.Object, mockUnitOfWork.Object);
        //Arrange
        CreateAnimalCommand command = new CreateAnimalCommand(
            "Test",
            "Test",
            "Test",
            "",
            "Test",
            "Test",
            new DateTimeOffset(),
            "Test",
            "Test",
            "Test");

        //Act
        var result = await handler.Handle(command, default);
        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Code.Should().Be(Errors.Animal.EmptySex.Code);
        result.FirstError.Description.Should().Be(Errors.Animal.EmptySex.Description);
    }

    [TestMethod]
    public async Task HandleCreateAnimal_WhenColorIsAnEmptyField_ShouldReturnValidationError()
    {
        var mockAnimalRepository = new Mock<IAnimalRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var handler = new CreateAnimalCommandHandler(
            mockAnimalRepository.Object, mockUnitOfWork.Object);
        //Arrange
        CreateAnimalCommand command = new CreateAnimalCommand(
            "Test",
            "Test",
            "Test",
            "Test",
            "",
            "Test",
            new DateTimeOffset(),
            "Test",
            "Test",
            "Test");

        //Act
        var result = await handler.Handle(command, default);
        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Code.Should().Be(Errors.Animal.EmptyColor.Code);
        result.FirstError.Description.Should().Be(Errors.Animal.EmptyColor.Description);
    }

    [TestMethod]
    public async Task HandleCreateAnimal_WhenInputIsCorrect_ShouldReturnOk()
    {
        var mockAnimalRepository = new Mock<IAnimalRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var handler = new CreateAnimalCommandHandler(
            mockAnimalRepository.Object, mockUnitOfWork.Object);
        //Arrange
        CreateAnimalCommand command = new CreateAnimalCommand(
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
        
        //Act
        var result = await handler.Handle(command, default);

        //Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().BeOfType<Unit>();
        mockAnimalRepository.Verify(
            repo => repo.Add(
                It.Is<Animal>(animal =>
                    animal.Name == command.Name &&
                    animal.Species == command.Species &&
                    animal.Breed == command.Breed &&
                    animal.Sex == command.Sex &&
                    animal.Color == command.Color &&
                    animal.Description == command.Description &&
                    animal.IntakeDate == command.IntakeDate &&
                    animal.AvailabilityStatus == command.AvailabilityStatus &&
                    animal.MedicalHistory == command.MedicalHistory &&
                    animal.SpecialNeeds == command.SpecialNeeds
               )),
            Times.Once);
    }
}
