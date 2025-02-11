using ErrorOr;
using MediatR;

namespace Application.Adopters.Update;

public record UpdateAdopterCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Country,
    string City,
    string Street,
    int HouseNumber,
    string Email
) : IRequest<ErrorOr<Unit>>;