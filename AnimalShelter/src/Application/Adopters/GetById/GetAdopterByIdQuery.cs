using Application.Common.Adopters;
using ErrorOr;
using MediatR;

namespace Application.Adopters.GetById;

public record GetAdopterByIdQuery (Guid Id) : IRequest<ErrorOr<AdopterResponse>>;