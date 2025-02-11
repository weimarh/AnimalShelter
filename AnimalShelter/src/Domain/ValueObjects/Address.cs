namespace Domain.ValueObjects;

public class Address
{
    public Address(string country, string city, string street, int houseNumber)
    {
        Country = country;
        City = city;
        Street = street;
        HouseNumber = houseNumber;
    }

    public string Country { get; init; } = null!;
    public string City { get; init; } = null!;
    public string Street { get; init; } = null!;
    public int HouseNumber { get; init; } 

    public static Address? Create(string country, string city, string street, int houseNumber)
    {
        if (string.IsNullOrWhiteSpace(country))
            return null;
        if (string.IsNullOrWhiteSpace(city))
            return null;
        if (string.IsNullOrWhiteSpace(street))
            return null;
        
        return new Address(country, city, street, houseNumber);
    }
}
