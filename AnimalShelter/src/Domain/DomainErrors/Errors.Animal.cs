
using ErrorOr;

namespace Domain.DomainErrors;
public static partial class Errors
{
    public static class Animal
    {
        public static Error EmptyName => 
            Error.Validation("Animal.Name", "The name is required");
        
        public static Error EmptySpecies =>
            Error.Validation("Animal.Species", "The species is required");
        
        public static Error EmptySex =>
            Error.Validation("Animal.Sex", "The sex is required");
        
        public static Error EmptyColor =>
            Error.Validation("Animal.Color", "The color is required");

        public static Error AnimalNotFound =>
            Error.NotFound("Animal.NotFound", "The animal was not found");
    }
}