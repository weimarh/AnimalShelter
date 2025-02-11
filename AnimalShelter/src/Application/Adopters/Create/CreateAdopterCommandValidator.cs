using FluentValidation;

namespace Application.Adopters.Create;

public class CreateAdopterCommandValidator : AbstractValidator<CreateAdopterCommand>
{
    public CreateAdopterCommandValidator()
    {
        RuleFor(r => r.FirstName).
            NotEmpty().
            MaximumLength(10).
            WithName("First name");
        
        RuleFor(r => r.LastName).
            NotEmpty().
            MaximumLength(10).
            WithName("Last name");
        
        RuleFor(r => r.PhoneNumber).
            NotEmpty().
            MaximumLength(8).
            WithName("Phone number");
        
        RuleFor(r => r.Country).
            NotEmpty().
            MaximumLength(20);
        
        RuleFor(r => r.City).
            NotEmpty().
            MaximumLength(20);

        RuleFor(r => r.Street).
            NotEmpty().
            MaximumLength(30);
    }
}
