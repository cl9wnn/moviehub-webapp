using API.Models.Requests;
using FluentValidation;

namespace API.Validation;

public class CreateCommentValidator : AbstractValidator<CreateCommentRequest>
{
    public CreateCommentValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage("Content is required.")
            .MaximumLength(1024)
            .WithMessage("Content must be maximum 1024 characters long.");
    }

}