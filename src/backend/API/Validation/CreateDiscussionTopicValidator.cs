using API.Models.Requests;
using FluentValidation;

namespace API.Validation;

public class CreateDiscussionTopicValidator: AbstractValidator<CreateDiscussionTopicRequest>
{
    public CreateDiscussionTopicValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required.")
            .MaximumLength(128)
            .WithMessage("Title must be maximum 128 characters long.");
        
        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage("Content is required.")
            .MaximumLength(1024)
            .WithMessage("Content must be maximum 1024 characters long.");
        
        RuleFor(u => u.TagIds)
            .NotNull().
            WithMessage("TopicIds is required.")
            .Must(g => g.Count <= 3)
            .WithMessage("TopicIds must contain 3 or less items.");
    }
}