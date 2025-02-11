using Application.Common.Adoptions;
using Domain.Adoptions;
using Domain.DomainErrors;
using ErrorOr;
using MediatR;

namespace Application.Adoptions.GetById;

public class GetAdopterByIdQueryHandler :
    IRequestHandler<GetAdoptionByIdQuery, ErrorOr<AdoptionResponse>>
{
    private readonly IAdoptionRepository _adoptionRepository;

    public GetAdopterByIdQueryHandler(IAdoptionRepository adoptionRepository)
    {
        _adoptionRepository = adoptionRepository ?? throw new ArgumentNullException(nameof(adoptionRepository));
    }

    public async Task<ErrorOr<AdoptionResponse>> Handle(GetAdoptionByIdQuery request, CancellationToken cancellationToken)
    {
        if (await _adoptionRepository.GetByIdAsync(new AdoptionId(request.Id)) is not Adoption adoption)
        {
            return Errors.Adoption.AdoptionNotFound;
        }

        return new AdoptionResponse(
            adoption.Id.Id,
            adoption.AnimalId.Id,
            adoption.AdopterId.Id,
            adoption.AdoptionDate.ToString()
        );
    }
}
