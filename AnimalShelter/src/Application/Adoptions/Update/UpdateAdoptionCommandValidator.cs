using FluentValidation;

namespace Application.Adoptions.Update;

public class UpdateAdoptionCommandValidator : AbstractValidator<UpdateAdoptionCommand>
{
    public UpdateAdoptionCommandValidator()
    {
        RuleFor(x => x.Id).
            NotEmpty().
            WithMessage("Id is required");
        
        RuleFor(x => x.AdopterId).
            NotEmpty().
            WithMessage("Adopter Id is required");

        RuleFor(x => x.AnimalId).
            NotEmpty().
            WithMessage("Animal Id is required");
    }
}
