using FluentValidation;

namespace Application.Animals.Create;

public class CreateAnimalCommandValidator : AbstractValidator<CreateAnimalCommand>
{
    public CreateAnimalCommandValidator()
    {
        RuleFor(x => x.Name).
            MaximumLength(50);

        RuleFor(x => x.Species).
            NotEmpty().
            MaximumLength(20);
        
        RuleFor(x => x.Breed).
            MaximumLength(50);

        RuleFor(x => x.Sex).
            NotEmpty().
            MaximumLength(10);
        
        RuleFor(x => x.Color).
            NotEmpty().
            MaximumLength(50);
        
        RuleFor(x => x.Description).
            MaximumLength(200);
        
        RuleFor(x => x.IntakeDate).
            NotEmpty();
        
        RuleFor(x => x.AvailabilityStatus).
            NotEmpty().
            MaximumLength(20).
            WithName("Availability Status");
        
        RuleFor(x => x.MedicalHistory).
            MaximumLength(200).
            WithName("Medical history");

        RuleFor(x => x.SpecialNeeds).
            MaximumLength(150).
            WithName("Special needs");
    }
}
