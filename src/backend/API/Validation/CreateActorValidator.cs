using API.Models;
using API.Models.Requests;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;

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
            .Must(url => url == null || Uri.IsWellFormedUriString(url, UriKind.Absolute))
            .WithMessage("PhotoUrl must be a valid URL.");

        RuleForEach(x => x.Photos)
            .Must(url => !string.IsNullOrWhiteSpace(url) &&  Uri.IsWellFormedUriString(url, UriKind.Absolute))
            .WithMessage("Each photo must be a valid non-empty URL.")
            .When(x => x.Photos != null);
    }
}