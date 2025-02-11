using Application.Adopters.Delete;
using FluentValidation;

namespace Application.Adopters.GetById;

public class DeleteAnimalCommandValidator : AbstractValidator<DeleteAdopterCommand>
{
    public DeleteAnimalCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
    }
}
