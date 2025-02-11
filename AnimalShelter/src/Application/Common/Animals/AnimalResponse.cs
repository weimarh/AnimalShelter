namespace Application.Common.Animals;

public record AnimalResponse(
    Guid Id,
    string Name,
    string Species,
    string? Breed,
    string Sex,
    string Color,
    string? Description,
    string IntakeDate,
    string AvailabilityStatus,
    string? MedicalHistory,
    string? SpecialNeeds
);
