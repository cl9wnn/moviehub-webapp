using API.Models;
using API.Models.Requests;
using FluentValidation;

namespace API.Validation;

public class CreateActorValidator: AbstractValidator<CreateActorRequest>
{
    public CreateActorValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Name is required")
            .Length(2, 50);

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("LastName is required")
            .Length(2, 50);

        RuleFor(x => x.Biography)
            .NotEmpty()
            .MinimumLength(10);

        RuleFor(x => x.BirthDate)
            .NotEmpty()
            .LessThan(DateOnly.FromDateTime(DateTime.Now));

        RuleFor(x => x.PhotoUrl)
            .NotEmpty()
            .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
            .WithMessage("PhotoUrl must be a valid URL.");
        
        RuleFor(x => x.Photos)
            .NotEmpty().WithMessage("At least one additional photo is required");

        RuleForEach(x => x.Photos)
            .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
            .WithMessage("Invalid URL format in additional photos");
    }
}