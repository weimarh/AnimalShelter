using Domain.Adopters;
using Domain.Animals;
using Domain.Primitives;

namespace Domain.Adoptions;

public class Adoption : AggregateRoot
{
    private Adoption()
    {}
    
    public Adoption(
        AdoptionId id,
        AnimalId animalId,
        AdopterId adopterId,
        DateTimeOffset adoptionDate)
    {
        Id = id;
        AnimalId = animalId;
        AdopterId = adopterId;
        AdoptionDate = adoptionDate;
    }

    public AdoptionId Id { get; private set; } = null!;
    public AnimalId AnimalId { get; private set; } = null!;
    public AdopterId AdopterId{ get; private set; } = null!;
    public DateTimeOffset AdoptionDate { get; private set; }

    public static Adoption UpdateAdoption(
        Guid id,
        AnimalId animalId,
        AdopterId adopterId,
        DateTimeOffset adoptionDate)
    {
        return new Adoption(new AdoptionId(id), animalId, adopterId, adoptionDate);
    }

}
