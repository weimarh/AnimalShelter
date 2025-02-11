using Application.Common.Adopters;
using Domain.Adopters;
using ErrorOr;
using MediatR;

namespace Application.Adopters.GetAll;

public class GetAllAdoptersQueryHandler :
    IRequestHandler<GetAllAdoptersQuery, ErrorOr<IReadOnlyList<AdopterResponse>>>
{
    private readonly IAdopterRepository _adopterRepository;

    public GetAllAdoptersQueryHandler(IAdopterRepository adopterRepository)
    {
        _adopterRepository = adopterRepository ?? 
            throw new ArgumentNullException(nameof(adopterRepository));
    }

    public async Task<ErrorOr<IReadOnlyList<AdopterResponse>>> Handle(
        GetAllAdoptersQuery request, 
        CancellationToken cancellationToken)
    {
        IReadOnlyList<Adopter> adopters = await _adopterRepository.GetAllAsync();
        
        return adopters.Select(adopter => new AdopterResponse(
            adopter.Id.Id,
            adopter.FirstName,
            adopter.LastName,
            adopter.PhoneNumber.Value,
            new AddressResponse(
                adopter.Address.Country,
                adopter.Address.City,
                adopter.Address.Street,
                adopter.Address.HouseNumber.ToString()
            ),
            adopter.Email
        )).ToList();
    }
}
