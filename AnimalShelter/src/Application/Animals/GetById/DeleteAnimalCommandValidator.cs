using Application.Animals.Delete;
using FluentValidation;

namespace Application.Animals.GetById;

public class DeleteAnimalCommandValidator : AbstractValidator<DeleteAnimalCommand>
{
    public DeleteAnimalCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is Required");
    }
}
