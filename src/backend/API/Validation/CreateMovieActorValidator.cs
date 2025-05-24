using API.Models.Requests;
using FluentValidation;

namespace API.Validation;

public class CreateMovieActorValidator: AbstractValidator<CreateMovieActorRequest>
{
    public CreateMovieActorValidator()
    {
        RuleFor(x => x.CharacterName)
            .NotEmpty().WithMessage("CharacterName is required")
            .MaximumLength(100);

        RuleFor(x => x.Actor)
            .SetValidator(new CreateActorValidator());
    }
}