using FluentValidation;

namespace Application.Animals.Delete;

public class DeleteAnimalCommandValidator : AbstractValidator<DeleteAnimalCommand>
{
    public DeleteAnimalCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
    }
}
