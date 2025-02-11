using Domain.Primitives;

namespace Domain.Animals;

public sealed class Animal : AggregateRoot
{
    private Animal()
    {
    }

    public Animal(
        AnimalId id,
        string? name,
        string species,
        string? breed,
        string sex,
        string? color,
        string? description,
        DateTimeOffset intakeDate,
        string? availabilityStatus,
        string? medicalHistory,
        string? specialNeeds)
    {
        Id = id;
        Name = name;
        Species = species;
        Breed = breed;
        Sex = sex;
        Color = color;
        Description = description;
        IntakeDate = intakeDate;
        AvailabilityStatus = availabilityStatus;
        MedicalHistory = medicalHistory;
        SpecialNeeds = specialNeeds;
    }
    
    public AnimalId Id { get; private set; } = null!;
    public string? Name { get; private set; }
    public string Species { get; private set; } = null!;
    public string? Breed { get; private set; }
    public string Sex { get; private set; } = null!;
    public string? Color { get; private set; }
    public string? Description { get; private set; }
    public DateTimeOffset IntakeDate { get; private set; }
    public string? AvailabilityStatus { get; private set; }
    public string? MedicalHistory { get; private set; }
    public string? SpecialNeeds { get; private set; }

    public static Animal UpdateAnimal(
        Guid id,
        string? name,
        string species,
        string? breed,
        string sex,
        string? color,
        string? description,
        DateTimeOffset intakeDate,
        string? availabilityStatus,
        string? medicalHistory,
        string? specialNeeds)
    {
        return new Animal(
            new AnimalId(id),
            name,
            species,
            breed,
            sex,
            color,
            description,
            intakeDate,
            availabilityStatus,
            medicalHistory,
            specialNeeds
        );
    }
}
