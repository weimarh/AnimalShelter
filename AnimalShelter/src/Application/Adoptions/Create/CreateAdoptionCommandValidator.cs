using FluentValidation;

namespace Application.Adoptions.Create;

public class CreateAdoptionCommandValidator : AbstractValidator<CreateAdoptionCommand>
{
    public CreateAdoptionCommandValidator()
    {
        RuleFor(x => x.AdopterId).NotEmpty();
        RuleFor(x => x.AnimalId).NotEmpty();
    }
}
