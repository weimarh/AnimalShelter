using FluentValidation;

namespace Application.Adopters.Update;

public class UpdateAdopterCommandValidator : AbstractValidator<UpdateAdopterCommand>
{
    public UpdateAdopterCommandValidator()
    {
        RuleFor(x => x.Id).
            NotEmpty().
            WithMessage("Id Is Required");

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
