using System.Data;
using API.Models.Requests;
using FluentValidation;

namespace API.Validation;

public class PersonalizeUserValidator: AbstractValidator<PersonalizeUserRequest>
{
    public PersonalizeUserValidator()
    {
        RuleFor(u => u.Bio)
            .NotEmpty()
            .MaximumLength(256)
            .WithMessage("Bio must be maximum 256 characters long.");

        RuleFor(u => u.Genres)
            .NotNull().
            WithMessage("GenreIds is required.")
            .Must(g => g.Count == 3)
            .WithMessage("GenreIds must contain exactly 3 items.");
    }
}