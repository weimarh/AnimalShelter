using Domain.Adopters;
using Domain.Primitives;
using ErrorOr;
using MediatR;
using Domain.DomainErrors;
using Domain.ValueObjects;

namespace Application.Adopters.Update;

public class UpdateAdopterCommandHandler :
    IRequestHandler<UpdateAdopterCommand, ErrorOr<Unit>>
{
    private readonly IAdopterRepository _adopterRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAdopterCommandHandler(
        IAdopterRepository adopterRepository,
        IUnitOfWork unitOfWork)
    {
        _adopterRepository = adopterRepository ?? throw new ArgumentNullException(nameof(adopterRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(
        UpdateAdopterCommand request,
        CancellationToken cancellationToken)
    {
        if (!await _adopterRepository.ExistsAsync(new AdopterId(request.Id)))
            return Errors.Adopter.AdopterNotFound;
        
        if (PhoneNumber.Create(request.PhoneNumber) is not PhoneNumber phoneNumber)
            return Errors.Adopter.PhoneNumberWithBadFormat;
        

        if (Address.Create(
            request.Country, request.City, request.Street, request.HouseNumber) 
            is not Address address)
            return Errors.Adopter.AddressWithBadFormat;

        if (string.IsNullOrWhiteSpace(request.FirstName))
            return Errors.Adopter.EmptyName;
        
        if (string.IsNullOrWhiteSpace(request.LastName))
            return Errors.Adopter.EmptyLastName;
        
        Adopter adopter = Adopter.UpdateAdopter(
            request.Id,
            request.FirstName,
            request.LastName,
            phoneNumber,
            address,
            request.Email
        );

        _adopterRepository.Update(adopter);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
