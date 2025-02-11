using Application.Common.Adoptions;
using ErrorOr;
using MediatR;

namespace Application.Adoptions.GetById;

public record GetAdoptionByIdQuery (Guid Id) : IRequest<ErrorOr<AdoptionResponse>>;