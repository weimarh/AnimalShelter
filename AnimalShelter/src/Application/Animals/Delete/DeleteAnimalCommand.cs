using Application.Common.Animals;
using ErrorOr;
using MediatR;

namespace Application.Animals.Delete;

public record DeleteAnimalCommand(Guid Id) : IRequest<ErrorOr<Unit>>;