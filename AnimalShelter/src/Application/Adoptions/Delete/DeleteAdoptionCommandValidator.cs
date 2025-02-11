using FluentValidation;

namespace Application.Adoptions.Delete;

public class DeleteAdoptionCommandValidator : AbstractValidator<DeleteAdoptionCommand>
{
    public DeleteAdoptionCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
    }
}
