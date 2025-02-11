using Domain.Adoptions;
using Domain.Primitives;
using ErrorOr;
using MediatR;
using Domain.DomainErrors;

namespace Application.Adoptions.Delete;

public class DeleteAdoptionCommandHandler :
    IRequestHandler<DeleteAdoptionCommand, ErrorOr<Unit>>
{
    private readonly IAdoptionRepository _adoptionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAdoptionCommandHandler(
        IAdoptionRepository adoptionRepository,
        IUnitOfWork unitOfWork)
    {
        _adoptionRepository = adoptionRepository ?? throw new ArgumentNullException(nameof(adoptionRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(
        DeleteAdoptionCommand request,
        CancellationToken cancellationToken)
    {
        if (await _adoptionRepository.GetByIdAsync(new AdoptionId(request.Id)) is not Adoption adoption)
        {
            return Errors.Adoption.AdoptionNotFound;
        }

        _adoptionRepository.Remove(adoption);

        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}
