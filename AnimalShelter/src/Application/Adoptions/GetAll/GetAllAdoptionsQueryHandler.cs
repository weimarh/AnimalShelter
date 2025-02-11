using Application.Common.Adoptions;
using Domain.Adoptions;
using ErrorOr;
using MediatR;

namespace Application.Adoptions.GetAll;

public class GetAllAdoptionsQueryHandler :
    IRequestHandler<GetAllAdoptionsQuery, ErrorOr<IReadOnlyList<AdoptionResponse>>>
{
    private readonly IAdoptionRepository _adoptionRepository;

    public GetAllAdoptionsQueryHandler(IAdoptionRepository adoptionRepository)
    {
        _adoptionRepository = adoptionRepository;
    }

    public async Task<ErrorOr<IReadOnlyList<AdoptionResponse>>> Handle(
        GetAllAdoptionsQuery request,
        CancellationToken cancellationToken)
    {
        IReadOnlyList<Adoption> adoptions = await _adoptionRepository.GetAllAsync();

        return adoptions.Select(adoption => new AdoptionResponse(
            adoption.Id.Id,
            adoption.AnimalId.Id,
            adoption.AdopterId.Id,
            adoption.AdoptionDate.ToString()
        )).ToList();
    }
}
