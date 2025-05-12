using Application.Abstractions;
using Application.Utils;
using Application.Validators;
using Domain.Abstractions;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class UserService(IUserRepository repository, IAuthService authService): IUserService
{
    public async Task<Result> RegisterAsync(string username, string email, string password)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = username,
            Email = email,
            Password = password,
        };
        
        var userValidator = new RegisterUserValidator();
        var validationResult = await userValidator.ValidateAsync(user);

        if (!validationResult.IsValid)
        {
            return Result.Failure(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));
        }
        
        var hashPassword = new PasswordHasher<User>().HashPassword(user, password);
        user.Password = hashPassword;
        
        var addResult = await repository.AddAsync(user);
        
        return addResult.IsSuccess
            ? Result.Success()
            : Result.Failure(addResult.ErrorMessage!);
    }

    public async Task<Result<string>> LoginAsync(string username, string password)
    {
        var userResult = await repository.GetByUsernameAsync(username);

        if (!userResult.IsSuccess)
        {
            return Result<string>.Failure(userResult.ErrorMessage!)!;
        }

        var user = userResult.Data;
        var verifyResult = new PasswordHasher<User>().VerifyHashedPassword(user, user.Password, password);

        if (verifyResult != PasswordVerificationResult.Success)
        {
            return Result<string>.Failure("Invalid password")!;
        }
        
        var token = authService.GenerateJwtToken(user);
        return Result<string>.Success(token);
    }
}