using Application.Adoptions.Delete;
using Domain.Adoptions;
using Domain.Primitives;
using Domain.Adopters;
using Domain.Animals;

namespace Application.Animals.UnitTests.AdoptionTests.Delete;

[TestClass]
public class DeleteAdoptionCommandHandlerTests
{
    [TestMethod]
    public async Task Handle_DeleteAdoptionCommand_ReturnsSuccess()
    {
        // Arrange
        var mockAdoptionRepository = new Mock<IAdoptionRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var handler = new DeleteAdoptionCommandHandler(
            mockAdoptionRepository.Object, mockUnitOfWork.Object);

        var AdoptionId = Guid.NewGuid();
        var AdopterId = Guid.NewGuid();
        var AnimalId = Guid.NewGuid();
        
        var adoption = new Adoption(
            new AdoptionId(AdoptionId),
            new AnimalId(AnimalId),
            new AdopterId(AdopterId),
            DateTime.Now
        );

        var command = new DeleteAdoptionCommand(AdoptionId);

        mockAdoptionRepository.Setup(repo => repo.GetByIdAsync(new AdoptionId(command.Id)))
            .ReturnsAsync(adoption);

        mockUnitOfWork.Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().Be(Unit.Value);

        mockAdoptionRepository.Verify(repo => repo.Remove(adoption), Times.Once);
        mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task HandleDeleteAdoption_WhenAdoptionDoesNotExist_ShouldThrowAdoptionNotFoundException()
    {
        var mockAdoptionRepository = new Mock<IAdoptionRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var handler = new DeleteAdoptionCommandHandler(
            mockAdoptionRepository.Object, mockUnitOfWork.Object);
        
        var command = new DeleteAdoptionCommand(Guid.NewGuid());

        mockAdoptionRepository.Setup(repo => repo.GetByIdAsync(new AdoptionId(command.Id)))
            .ReturnsAsync((Adoption?)null);
        // Act 
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().Contain(Errors.Adoption.AdoptionNotFound);
        mockAdoptionRepository.Verify(repo => repo.Remove(It.IsAny<Adoption>()), Times.Never);
            mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), 
            Times.Never);
    }

}
