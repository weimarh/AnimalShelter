using Domain.Animals;
using Domain.Primitives;
using ErrorOr;
using MediatR;
using Domain.DomainErrors;

namespace Application.Animals.Delete;

public sealed class DeleteAnimalCommandHandler :
    IRequestHandler<DeleteAnimalCommand, ErrorOr<Unit>>
{
    private readonly IAnimalRepository _animalRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAnimalCommandHandler(
        IAnimalRepository animalRepository,
        IUnitOfWork unitOfWork)
    {
        _animalRepository = animalRepository ?? throw new ArgumentNullException(nameof(animalRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(
        DeleteAnimalCommand request,
        CancellationToken cancellationToken)
    {
        if (await _animalRepository.GetByIdAsync(new AnimalId(request.Id)) is not Animal animal)
        {
            return Errors.Animal.AnimalNotFound;
        }

        _animalRepository.Remove(animal);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
