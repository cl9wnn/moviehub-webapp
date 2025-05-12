using Domain.Models;
using FluentValidation;

namespace Application.Validators;

public class RegisterUserValidator: AbstractValidator<User>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Invalid email address format.");
        
        RuleFor(user => user.Username)
            .NotEmpty()
            .Matches("^[a-zA-Z0-9_\\s]{5,20}$")
            .WithMessage("First name must beet 5 and 20 characters.");
        
        RuleFor(user => user.Password)
            .NotEmpty()
            .Matches(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
            .WithMessage("Password must be between 8 and 20 characters, " +
                         "at least one digit, special symbol, and upper case letter.");
            
    }
}