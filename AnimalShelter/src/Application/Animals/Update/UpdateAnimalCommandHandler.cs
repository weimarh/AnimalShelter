using Domain.Animals;
using Domain.Primitives;
using ErrorOr;
using MediatR;
using Domain.DomainErrors;

namespace Application.Animals.Update;

public class UpdateAnimalCommandHandler :
    IRequestHandler<UpdateAnimalCommand, ErrorOr<Unit>>
{
    private readonly IAnimalRepository _animalRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAnimalCommandHandler(
        IAnimalRepository animalRepository,
        IUnitOfWork unitOfWork)
    {
        _animalRepository = animalRepository ?? throw new ArgumentNullException(nameof(animalRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(
        UpdateAnimalCommand request,
        CancellationToken cancellationToken)
    {
        if (!await _animalRepository.ExistsAsync(new AnimalId(request.Id)))
            return Errors.Animal.AnimalNotFound;
        
        if (string.IsNullOrWhiteSpace(request.Name))
            return Errors.Animal.EmptyName;

        if (string.IsNullOrWhiteSpace(request.Species))
            return Errors.Animal.EmptySpecies;

        if (string.IsNullOrWhiteSpace(request.Sex))
            return Errors.Animal.EmptySex;

        if (string.IsNullOrWhiteSpace(request.Color))
            return Errors.Animal.EmptyColor;
        
        Animal animal = Animal.UpdateAnimal(
            request.Id,
            request.Name,
            request.Species,
            request.Breed,
            request.Sex,
            request.Color,
            request.Description,
            request.IntakeDate,
            request.AvailabilityStatus,
            request.MedicalHistory,
            request.SpecialNeeds
        );
    
        _animalRepository.Update(animal);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
