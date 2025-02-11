using Domain.Adopters;
using Domain.Adoptions;
using Domain.Animals;
using Domain.DomainErrors;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Adoptions.Create;

public class CreateAdoptionCommandHandler : IRequestHandler<CreateAdoptionCommand, ErrorOr<Unit>>
{
    private readonly IAdoptionRepository _adoptionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAdopterRepository _adopterRepository;
    private readonly IAnimalRepository _animalRepository;

    public CreateAdoptionCommandHandler(
        IAdoptionRepository adoptionRepository,
        IUnitOfWork unitOfWork,
        IAdopterRepository adopterRepository,
        IAnimalRepository animalRepository)
    {
        _adoptionRepository = adoptionRepository;
        _unitOfWork = unitOfWork;
        _adopterRepository = adopterRepository;
        _animalRepository = animalRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(
        CreateAdoptionCommand command,
        CancellationToken cancellationToken)
    { 
        if (command.AnimalId == string.Empty)
            return Errors.Adoption.EmptyAnimalId;

        if (command.AdopterId == string.Empty)
            return Errors.Adoption.EmptyAdopterId;
        
        var animalExists = await _animalRepository.ExistsAsync(
            new AnimalId(new Guid(command.AnimalId)));

        if (!animalExists)
            return Errors.Adoption.AnimalIdDoesNotExist;

        var adopterExists = await _adopterRepository.ExistsAsync(
            new AdopterId(new Guid(command.AdopterId)));

        if (!adopterExists)
            return Errors.Adoption.AdopterIdDoesNotExist;

        var adoption = new Adoption(
            new AdoptionId(Guid.NewGuid()),
            new AnimalId(new Guid(command.AnimalId)),
            new AdopterId(new Guid(command.AdopterId)),
            command.AdoptionDate);
        
        await _adoptionRepository.Add(adoption);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
