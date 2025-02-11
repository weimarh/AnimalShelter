namespace Application.Common.Adoptions;

public record AdoptionResponse(
    Guid Id,
    Guid AnimalId,
    Guid AdopterId,
    string AdoptionDate
);