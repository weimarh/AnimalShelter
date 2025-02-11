using ErrorOr;
using MediatR;

namespace Application.Animals.Update;

public record UpdateAnimalCommand(
    Guid Id,
    string Name,
    string Species,
    string Breed,
    string Sex,
    string Color,
    string Description,
    DateTimeOffset IntakeDate,
    string AvailabilityStatus,
    string MedicalHistory,
    string SpecialNeeds
) : IRequest<ErrorOr<Unit>>;