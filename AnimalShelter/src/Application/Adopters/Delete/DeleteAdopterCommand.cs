using ErrorOr;
using MediatR;

namespace Application.Adopters.Delete;

public record DeleteAdopterCommand(Guid Id) : IRequest<ErrorOr<Unit>>;