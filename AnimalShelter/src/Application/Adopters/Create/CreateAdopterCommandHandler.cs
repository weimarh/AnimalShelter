using Domain.Adopters;
using Domain.Primitives;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;
using Domain.DomainErrors;

namespace Application.Adopters.Create;

public sealed class CreateAdopterCommandHandler : IRequestHandler<CreateAdopterCommand, ErrorOr<Unit>>
{
    private readonly IAdopterRepository _adopterRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAdopterCommandHandler(
        IUnitOfWork unitOfWork,
        IAdopterRepository adopterRepository)
    {
        _unitOfWork = unitOfWork ?? 
            throw new ArgumentNullException(nameof(unitOfWork));
        _adopterRepository = adopterRepository ?? 
            throw new ArgumentNullException(nameof(adopterRepository));
    }

    public async Task<ErrorOr<Unit>> Handle(
        CreateAdopterCommand command, CancellationToken cancellationToken)
    {
        if (PhoneNumber.Create(command.PhoneNumber) is not PhoneNumber phoneNumber)
            return Errors.Adopter.PhoneNumberWithBadFormat;
        

        if (Address.Create(
            command.Country, command.City, command.Street, command.HouseNumber) 
            is not Address address)
            return Errors.Adopter.AddressWithBadFormat;

        if (string.IsNullOrWhiteSpace(command.FirstName))
            return Errors.Adopter.EmptyName;
        
        if (string.IsNullOrWhiteSpace(command.LastName))
            return Errors.Adopter.EmptyLastName;

        var adopter = new Adopter(
            new AdopterId(Guid.NewGuid()),
            command.FirstName,
            command.LastName,
            phoneNumber,
            address,
            command.Email);
        
        await _adopterRepository.Add(adopter);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
