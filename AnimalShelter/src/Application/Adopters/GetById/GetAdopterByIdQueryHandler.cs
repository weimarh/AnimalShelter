using Application.Common.Adopters;
using Domain.Adopters;
using Domain.DomainErrors;
using ErrorOr;
using MediatR;


namespace Application.Adopters.GetById;

public class GetAdopterByIdQueryHandler : 
    IRequestHandler<GetAdopterByIdQuery, ErrorOr<AdopterResponse>>
{
    private readonly IAdopterRepository _adopterRepository;

    public GetAdopterByIdQueryHandler(IAdopterRepository adopterRepository)
    {
        _adopterRepository = adopterRepository ?? throw new ArgumentNullException(nameof(adopterRepository));
    }

    public async Task<ErrorOr<AdopterResponse>> Handle(
        GetAdopterByIdQuery request,
        CancellationToken cancellationToken)
    {
        if (await _adopterRepository.GetByIdAsync(new AdopterId(request.Id)) is not Adopter adopter)
        {
            return Errors.Adopter.AdopterNotFound;
        }

        return new AdopterResponse(
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
            adopter.Email);
    }
}
