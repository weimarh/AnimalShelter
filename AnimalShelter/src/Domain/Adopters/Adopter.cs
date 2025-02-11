using Domain.Animals;
using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Adopters;

public sealed class Adopter : AggregateRoot
{
    private Adopter()
    {}

    public Adopter(
        AdopterId id,
        string firstName,
        string lastName,
        PhoneNumber phoneNumber,
        Address address,
        string email)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Address = address;
        Email = email;
    }
    
    public AdopterId Id { get; private set; } = null!;
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public PhoneNumber PhoneNumber { get; private set; } = null!;
    public Address Address { get; private set; } = null!;
    public string Email { get; private set; } = null!;

    public static Adopter UpdateAdopter(
        Guid id,
        string firstName,
        string lastName,
        PhoneNumber phoneNumber,
        Address address,
        string email)
    {
        return new Adopter(
            new AdopterId(id),
            firstName,
            lastName,
            phoneNumber,
            address,
            email);
    }

}
