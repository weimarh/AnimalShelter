using ErrorOr;
using MediatR;

namespace Application.Adopters.Create;

public record CreateAdopterCommand(
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Country,
    string City,
    string Street,
    int HouseNumber,
    string Email
) : IRequest<ErrorOr<Unit>>;
