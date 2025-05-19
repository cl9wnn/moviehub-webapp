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
    public async Task<Result> RegisterAsync(User userDto)
    {
        var userValidator = new RegisterUserValidator();
        var validationResult = await userValidator.ValidateAsync(userDto);

        if (!validationResult.IsValid)
        {
            return Result.Failure(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));
        }
        
        var hashPassword = new PasswordHasher<User>().HashPassword(userDto, userDto.Password);
        userDto.Password = hashPassword;
        
        var addResult = await userRepository.AddAsync(userDto);
        
        return addResult.IsSuccess
            ? Result.Success()
            : Result.Failure(addResult.ErrorMessage!);
    }
}