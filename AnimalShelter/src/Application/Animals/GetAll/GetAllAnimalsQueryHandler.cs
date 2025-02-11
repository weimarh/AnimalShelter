using Application.Common.Animals;
using Domain.Animals;
using ErrorOr;
using MediatR;

namespace Application.Animals.GetAll;

public class GetAllAnimalsQueryHandler :
    IRequestHandler<GetAllAnimalsQuery, ErrorOr<IReadOnlyList<AnimalResponse>>>
{
    private readonly IAnimalRepository _animalRepository;

    public GetAllAnimalsQueryHandler(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository ?? 
            throw new ArgumentNullException(nameof(animalRepository));
    }

    public async Task<ErrorOr<IReadOnlyList<AnimalResponse>>> Handle(
        GetAllAnimalsQuery request,
        CancellationToken cancellationToken)
    {
        IReadOnlyList<Animal> animals = await _animalRepository.GetAllAsync();

        return animals.Select(animal => new AnimalResponse(
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
            animal.SpecialNeeds
        )).ToList();
    }
}
