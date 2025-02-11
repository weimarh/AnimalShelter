using ErrorOr;
using MediatR;

namespace Application.Adoptions.Delete;

public record DeleteAdoptionCommand(Guid Id) : IRequest<ErrorOr<Unit>>;