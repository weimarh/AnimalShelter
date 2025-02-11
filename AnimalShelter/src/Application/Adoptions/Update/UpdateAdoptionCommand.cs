using ErrorOr;
using MediatR;

namespace Application.Adoptions.Update;

public record UpdateAdoptionCommand(
    Guid Id,
    string AnimalId,
    string AdopterId,
    DateTimeOffset AdoptionDate
) : IRequest<ErrorOr<Unit>>;