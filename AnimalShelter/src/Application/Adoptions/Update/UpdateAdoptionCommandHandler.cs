using Domain.Adoptions;
using Domain.Primitives;
using ErrorOr;
using MediatR;
using Domain.DomainErrors;
using Domain.Adopters;
using Domain.Animals;

namespace Application.Adoptions.Update;

public class UpdateAdoptionCommandHandler
    : IRequestHandler<UpdateAdoptionCommand, ErrorOr<Unit>>
{
    private readonly IAdoptionRepository _adoptionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAdopterRepository _adopterRepository;
    private readonly IAnimalRepository _animalRepository;

    public UpdateAdoptionCommandHandler(
        IAdoptionRepository adoptionRepository,
        IUnitOfWork unitOfWork,
        IAdopterRepository adopterRepository,
        IAnimalRepository animalRepository)
    {
        _adoptionRepository = adoptionRepository ?? throw new ArgumentNullException(nameof(adoptionRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _adopterRepository = adopterRepository ?? throw new ArgumentNullException(nameof(adopterRepository));
        _animalRepository = animalRepository ?? throw new ArgumentNullException(nameof(animalRepository));
    }

    public async Task<ErrorOr<Unit>> Handle(
        UpdateAdoptionCommand request,
        CancellationToken cancellationToken)
    {
        if (!await _adoptionRepository.ExistsAsync(new AdoptionId(request.Id)))
        {
            return Errors.Adoption.AdoptionNotFound;
        }

        if (request.AnimalId == string.Empty)
            return Errors.Adoption.EmptyAnimalId;

        if (request.AdopterId == string.Empty)
            return Errors.Adoption.EmptyAdopterId;
        
        var animalExists = await _animalRepository.ExistsAsync(
            new AnimalId(new Guid(request.AnimalId)));

        if (!animalExists)
            return Errors.Adoption.AnimalIdDoesNotExist;

        var adopterExists = await _adopterRepository.ExistsAsync(
            new AdopterId(new Guid(request.AdopterId)));

        if (!adopterExists)
            return Errors.Adoption.AdopterIdDoesNotExist;
        
        Adoption adoption = Adoption.UpdateAdoption(
            request.Id,
            new AnimalId(new Guid(request.AnimalId)),
            new AdopterId(new Guid(request.AdopterId)),
            request.AdoptionDate);
        
        _adoptionRepository.Update(adoption);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
