using Application.Common.Animals;
using Domain.Animals;
using ErrorOr;
using MediatR;
using Domain.DomainErrors;

namespace Application.Animals.GetById;

public class GetAnimalByIdQueryHandler : 
    IRequestHandler<GetAnimalByIdQuery, ErrorOr<AnimalResponse>>
{
    private readonly IAnimalRepository _animalRepository;

    public GetAnimalByIdQueryHandler(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository ?? throw new ArgumentNullException(nameof(animalRepository));
    }

    public async Task<ErrorOr<AnimalResponse>> Handle(
        GetAnimalByIdQuery request,
        CancellationToken cancellationToken)
    {
        if (await _animalRepository.GetByIdAsync(new AnimalId(request.Id)) is not Animal animal)
        {
            return Errors.Animal.AnimalNotFound;
        }

        return new AnimalResponse(
            animal.Id.Id,
            animal.Name,
            animal.Species,
            animal.Breed,
            animal.Sex,
            animal.Color,
            animal.Description,
            animal.IntakeDate.ToString(),
            animal.AvailabilityStatus,
            animal.MedicalHistory,
            animal.SpecialNeeds);
    }
}
