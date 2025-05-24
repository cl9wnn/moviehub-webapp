using Application.Utils;
using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class UserService(IUserRepository userRepository): IUserService
{
    public async Task<Result> RegisterAsync(User userDto)
    {
        var hashPassword = new PasswordHasher<User>().HashPassword(userDto, userDto.Password);
        userDto.Password = hashPassword;
        
        var addResult = await userRepository.AddAsync(userDto);
        
        return addResult.IsSuccess
            ? Result.Success()
            : Result.Failure(addResult.ErrorMessage!);
    }
}