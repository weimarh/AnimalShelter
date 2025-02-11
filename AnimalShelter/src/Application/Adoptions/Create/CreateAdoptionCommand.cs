using ErrorOr;
using MediatR;

namespace Application.Adoptions.Create;

public record CreateAdoptionCommand(
    string AnimalId,
    string AdopterId,
    DateTimeOffset AdoptionDate
) : IRequest<ErrorOr<Unit>>;