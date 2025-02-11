using Application.Common.Animals;
using ErrorOr;
using MediatR;

namespace Application.Animals.GetAll;

public record GetAllAnimalsQuery() : IRequest<ErrorOr<IReadOnlyList<AnimalResponse>>>;