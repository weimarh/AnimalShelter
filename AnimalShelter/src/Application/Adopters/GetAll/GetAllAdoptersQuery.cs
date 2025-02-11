using Application.Common.Adopters;
using ErrorOr;
using MediatR;

namespace Application.Adopters.GetAll;

public record GetAllAdoptersQuery() : IRequest<ErrorOr<IReadOnlyList<AdopterResponse>>>;