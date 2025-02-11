using Application.Common.Adoptions;
using ErrorOr;
using MediatR;

namespace Application.Adoptions.GetAll;

public record GetAllAdoptionsQuery() : IRequest<ErrorOr<IReadOnlyList<AdoptionResponse>>>;