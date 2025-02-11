using Domain.Adopters;
using Domain.Primitives;
using ErrorOr;
using MediatR;
using Domain.DomainErrors;

namespace Application.Adopters.Delete;

public class DeleteAdopterCommandHandler
    : IRequestHandler<DeleteAdopterCommand, ErrorOr<Unit>>
{
    private readonly IAdopterRepository _adopterRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAdopterCommandHandler(
        IAdopterRepository adopterRepository,
        IUnitOfWork unitOfWork)
    {
        _adopterRepository = adopterRepository ?? throw new ArgumentNullException(nameof(adopterRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(
        DeleteAdopterCommand request,
        CancellationToken cancellationToken)
    {
        if (await _adopterRepository.GetByIdAsync(new AdopterId(request.Id)) is not Adopter adopter)
        {
            return Errors.Adopter.AdopterNotFound;
        }

        _adopterRepository.Remove(adopter);

        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}
