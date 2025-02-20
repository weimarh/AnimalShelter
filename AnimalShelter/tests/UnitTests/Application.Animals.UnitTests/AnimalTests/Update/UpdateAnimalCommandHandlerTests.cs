using Application.Animals.Update;
using Domain.Animals;
using Domain.Primitives;

namespace Application.Animals.UnitTests.AnimalTests.Update;

[TestClass]
public class UpdateAnimalCommandHandlerTests
{
    [TestMethod]
    public async Task HandleUpdateAnimal_WhenAnimalDoesNotExist_ShouldThrowAnimalNotFoundException()
    {
        // Arrange
        var mockAnimalRepository = new Mock<IAnimalRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var handler = new UpdateAnimalCommandHandler(
            mockAnimalRepository.Object, mockUnitOfWork.Object);

        UpdateAnimalCommand command = new UpdateAnimalCommand(
            Guid.NewGuid(),
            "test",
            "test",
            "test",
            "test",
            "test",
            "test",
            DateTimeOffset.Now,
            "test",
            "test",
            "test");

        mockAnimalRepository.Setup(repo => repo.GetByIdAsync(new AnimalId(command.Id)))
            .ReturnsAsync((Animal?)null);
        // Act 
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().Contain(Errors.Animal.AnimalNotFound);
        mockAnimalRepository.Verify(repo => repo.Remove(It.IsAny<Animal>()), Times.Never);
            mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), 
            Times.Never);
    }

    [TestMethod]
        public async Task Handle_AnimalExists_UpdatesAnimalAndReturnsUnit()
        {
            // Arrange
            var mockAnimalRepository = new Mock<IAnimalRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var handler = new UpdateAnimalCommandHandler(mockAnimalRepository.Object, mockUnitOfWork.Object);

            var animalId = Guid.NewGuid();
            var command = new UpdateAnimalCommand(
                animalId,
                "UpdatedName",
                "UpdatedSpecies",
                "UpdatedBreed",
                "UpdatedSex",
                "UpdatedColor",
                "UpdatedDescription",
                DateTimeOffset.Now,
                "UpdatedAvailabilityStatus",
                "UpdatedMedicalHistory",
                "UpdatedSpecialNeeds");

            var existingAnimal = new Animal(
            new AnimalId(
                Guid.NewGuid()),
                "test",
                "test",
                "test",
                "test",
                "test",
                "test",
                DateTimeOffset.Now,
                "test",
                "test",
                "test");
            mockAnimalRepository.Setup(repo => repo.ExistsAsync(new AnimalId(command.Id)))
                .ReturnsAsync(true);
            mockAnimalRepository.Setup(repo => repo.GetByIdAsync(new AnimalId(command.Id)))
                .ReturnsAsync(existingAnimal);

            mockUnitOfWork.Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().Be(Unit.Value);

            mockAnimalRepository.Verify(repo => repo.Update(It.Is<Animal>(animal =>
                animal.Id.Id == command.Id &&
                animal.Name == command.Name &&
                animal.Species == command.Species &&
                animal.Breed == command.Breed &&
                animal.Sex == command.Sex &&
                animal.Color == command.Color &&
                animal.Description == command.Description &&
                animal.IntakeDate == command.IntakeDate &&
                animal.AvailabilityStatus == command.AvailabilityStatus &&
                animal.MedicalHistory == command.MedicalHistory &&
                animal.SpecialNeeds == command.SpecialNeeds)), Times.Once);

            mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

}
