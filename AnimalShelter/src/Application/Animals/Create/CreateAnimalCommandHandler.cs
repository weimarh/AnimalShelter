using Domain.Animals;
using Domain.Primitives;
using ErrorOr;
using MediatR;
using Domain.DomainErrors;

namespace Application.Animals.Create;

public class CreateAnimalCommandHandler : IRequestHandler<CreateAnimalCommand, ErrorOr<Unit>>
{
    private readonly IAnimalRepository _animalRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAnimalCommandHandler(
        IAnimalRepository animalRepository,
        IUnitOfWork unitOfWork)
    {
        _animalRepository = animalRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Unit>> Handle(CreateAnimalCommand command, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(command.Name))
            return Errors.Animal.EmptyName;

        if (string.IsNullOrWhiteSpace(command.Species))
            return Errors.Animal.EmptySpecies;

        if (string.IsNullOrWhiteSpace(command.Sex))
            return Errors.Animal.EmptySex;

        if (string.IsNullOrWhiteSpace(command.Color))
            return Errors.Animal.EmptyColor;

        var animal = new Animal(
        new AnimalId(Guid.NewGuid()),
        command.Name,
        command.Species,
        command.Breed,
        command.Sex,
        command.Color,
        command.Description,
        command.IntakeDate,
        command.AvailabilityStatus,
        command.MedicalHistory,
        command.SpecialNeeds);
    
        await _animalRepository.Add(animal);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
