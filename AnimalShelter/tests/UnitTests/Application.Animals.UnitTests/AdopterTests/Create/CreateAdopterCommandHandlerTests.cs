using Application.Adopters.Create;
using Domain.Adopters;
using Domain.Primitives;
using Domain.ValueObjects;

namespace Application.Animals.UnitTests.AdopterTests.Create;

[TestClass]
public class CreateAdopterCommandHandlerTests
{
    [TestMethod]
    public async Task HandleCreateAdpoter_WhenFirstNameIsAnEmptyField_ShouldReturnValidationError()
    {
        var mockAdopterRepository = new Mock<IAdopterRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var handler = new CreateAdopterCommandHandler(
            mockUnitOfWork.Object,
            mockAdopterRepository.Object);
        
        //Arrange
        CreateAdopterCommand command = new CreateAdopterCommand(
            "",
            "Test",
            "70792462",
            "Test",
            "Test",
            "Test",
            1243,
            "Test");
        
        //Act
        var result = await handler.Handle(command, default);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Code.Should().Be(Errors.Adopter.EmptyName.Code);
        result.FirstError.Description.Should().Be(Errors.Adopter.EmptyName.Description);
    }

    [TestMethod]
    public async Task HandleCreateAdpoter_WhenLastNameIsAnEmptyField_ShouldReturnValidationError()
    {
        var mockAdopterRepository = new Mock<IAdopterRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var handler = new CreateAdopterCommandHandler(
            mockUnitOfWork.Object,
            mockAdopterRepository.Object);
        
        //Arrange
        CreateAdopterCommand command = new CreateAdopterCommand(
            "Test",
            "",
            "70792462",
            "Test",
            "Test",
            "Test",
            1243,
            "Test");
        
        //Act
        var result = await handler.Handle(command, default);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Code.Should().Be(Errors.Adopter.EmptyLastName.Code);
        result.FirstError.Description.Should().Be(Errors.Adopter.EmptyLastName.Description);
    }

    [TestMethod]
    public async Task HandleCreateAdpoter_WhenPhoneNumberHasBadFormat_ShouldReturnValidationError()
    {
        var mockAdopterRepository = new Mock<IAdopterRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var handler = new CreateAdopterCommandHandler(
            mockUnitOfWork.Object,
            mockAdopterRepository.Object);
        
        //Arrange
        CreateAdopterCommand command = new CreateAdopterCommand(
            "Test",
            "Test",
            "7079246",
            "Test",
            "Test",
            "Test",
            1243,
            "Test");
        
        //Act
        var result = await handler.Handle(command, default);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Code.Should().Be(Errors.Adopter.PhoneNumberWithBadFormat.Code);
        result.FirstError.Description.Should().Be(Errors.Adopter.PhoneNumberWithBadFormat.Description);
    }

    [TestMethod]
    public async Task HandleCreateAdpoter_WhenAddressHasBadFormat_ShouldReturnValidationError()
    {
        var mockAdopterRepository = new Mock<IAdopterRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var handler = new CreateAdopterCommandHandler(
            mockUnitOfWork.Object,
            mockAdopterRepository.Object);
        
        //Arrange
        CreateAdopterCommand command = new CreateAdopterCommand(
            "Test",
            "Test",
            "70792462",
            "Test",
            "Test",
            "",
            1243,
            "Test");
        
        //Act
        var result = await handler.Handle(command, default);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Code.Should().Be(Errors.Adopter.AddressWithBadFormat.Code);
        result.FirstError.Description.Should().Be(Errors.Adopter.AddressWithBadFormat.Description);
    }

    [TestMethod]
    public async Task HandleCreateAdopter_WhenInputIsCorrect_ShouldReturnOk()
    {
        var mockAdopterRepository = new Mock<IAdopterRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var handler = new CreateAdopterCommandHandler(
            mockUnitOfWork.Object,
            mockAdopterRepository.Object);
        
        //Arrange
        CreateAdopterCommand command = new CreateAdopterCommand(
            "Test",
            "Test",
            "70792462",
            "Test",
            "Test",
            "Test",
            1243,
            "Test");
        
        //Act
        var result = await handler.Handle(command, default);

        //Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().BeOfType<Unit>();
        mockAdopterRepository.Setup(repo => repo.Add(
        It.Is<Adopter>(adopter =>
            adopter.FirstName == command.FirstName &&
            adopter.LastName == command.LastName &&
            adopter.PhoneNumber == PhoneNumber.Create(command.PhoneNumber) &&
            adopter.Address == Address.Create(
                command.Country,
                command.City,
                command.Street,
                command.HouseNumber) &&
            adopter.Email == command.Email
        )))
        .Returns(Task.CompletedTask);
        
    }
}
