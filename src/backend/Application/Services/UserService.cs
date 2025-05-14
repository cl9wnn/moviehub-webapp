using Application.Abstractions;
using Application.Utils;
using Application.Validators;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.Services;

public class UserService(IUserRepository userRepository): IUserService
{
    public async Task<Result> RegisterAsync(string username, string email, string password)
    {
        var user = User.Create(username, email, password);
        
        var userValidator = new RegisterUserValidator();
        var validationResult = await userValidator.ValidateAsync(user);

        if (!validationResult.IsValid)
        {
            return Result.Failure(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));
        }
        
        var hashPassword = new PasswordHasher<User>().HashPassword(user, password);
        user.Password = hashPassword;
        
        var addResult = await userRepository.AddAsync(user);
        
        return addResult.IsSuccess
            ? Result.Success()
            : Result.Failure(addResult.ErrorMessage!);
    }
}