namespace Application.Common.Adopters;

public record AdopterResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string PhoneNumber,
    AddressResponse Address,
    string Email
);

public record AddressResponse(
    string Country,
    string City,
    string Street,
    string HouseNumber
);