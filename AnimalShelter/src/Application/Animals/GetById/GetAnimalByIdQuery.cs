using Application.Common.Animals;
using ErrorOr;
using MediatR;

namespace Application.Animals.GetById;

public record GetAnimalByIdQuery(Guid Id) : IRequest<ErrorOr<AnimalResponse>>;