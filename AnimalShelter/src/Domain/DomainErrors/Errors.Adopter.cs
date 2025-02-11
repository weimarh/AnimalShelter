using ErrorOr;

namespace Domain.DomainErrors;

public static partial class Errors
{
    public static class Adopter
    {
        public static Error PhoneNumberWithBadFormat => Error.Validation(
            code: "Adopter.PhoneNumberWithBadFormat",
            description: "Phone number with bad format"
        );

        public static Error AddressWithBadFormat => Error.Validation(
            code: "Adopter.AddressWithBadFormat",
            description: "Address with bad format"
        );

        public static Error EmptyName => Error.Validation(
            code: "Adopter.EmptyName",
            description: "Name is empty"
        );

        public static Error EmptyLastName => Error.Validation(
            code: "Adopter.EmptyLastName",
            description: "Last Name is empty"
        );

        public static Error AdopterNotFound => Error.NotFound(
            code: "Adopter.AdopterNotFound",
            description: "Adopter not found"
        );
    }
}
