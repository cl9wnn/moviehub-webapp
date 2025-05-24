using API.Models.Requests;
using FluentValidation;

namespace API.Validation;

public class CreateMovieValidator: AbstractValidator<CreateMovieRequest>
{
    public CreateMovieValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200).WithMessage("Title is required");
        
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        
        RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);

        RuleFor(x => x.Year).InclusiveBetween(1888, DateTime.UtcNow.Year + 1);
        
        RuleFor(x => x.DurationAtMinutes).InclusiveBetween(1, 300);

        RuleFor(x => x.AgeRating).NotEmpty().MaximumLength(5);
        
        RuleFor(x => x.PosterUrl)
            .NotEmpty().Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
            .WithMessage("PosterUrl must be a valid URL.");
        
        RuleForEach(x => x.Photos)
            .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
            .WithMessage("Invalid URL format in additional photos");
        
        RuleFor(x => x.GenreIds)
            .NotEmpty().WithMessage("At least one genre is required.");
        
        RuleForEach(x => x.Directors)
            .ChildRules(director =>
            {
                director.RuleFor(d => d.FirstName)
                    .NotEmpty()
                    .MaximumLength(100);

                director.RuleFor(d => d.LastName)
                    .NotEmpty()
                    .MaximumLength(100);
            });
        
        RuleForEach(x => x.Writers)
            .ChildRules(writer =>
            {
                writer.RuleFor(w => w.FirstName)
                    .NotEmpty()
                    .MaximumLength(100);

                writer.RuleFor(w => w.LastName)
                    .NotEmpty()
                    .MaximumLength(100);
            });

        RuleForEach(x => x.Actors)
            .SetValidator(new CreateMovieActorValidator());
    }
}