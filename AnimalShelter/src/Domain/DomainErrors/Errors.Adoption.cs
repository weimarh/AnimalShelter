using ErrorOr;

namespace Domain.DomainErrors;

public static partial class Errors
{
    public static class Adoption
    {
        public static Error EmptyAnimalId =>
            Error.Validation("Adoption.AnimalId", "Animal Id cannot be empty.");
        
        public static Error EmptyAdopterId =>
            Error.Validation("Adoption.AdopterId", "Adopter Id cannot be empty.");
        
        public static Error AnimalIdDoesNotExist =>
            Error.Validation("Adoption.AnimalId", "Animal Id does not exist.");
        
        public static Error AdopterIdDoesNotExist =>
            Error.Validation("Adoption.AdopterId", "Adopter Id does not exist.");
        
        public static Error AdoptionNotFound =>
            Error.NotFound("Adoption.NotFound", "Adoption not found.");
    }
}
