using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
using Domain.Models;
using Domain.Utils;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class UserService(IUserRepository userRepository): IUserService
{
    public async Task<Result<List<User>>> GetAllUsersAsync()
    {
        var getResult = await userRepository.GetAllAsync();
        
        return Result<List<User>>.Success(getResult.Data.ToList());
    }

    public async Task<Result<User>> GetUserAsync(Guid id)
    {
        var getResult = await userRepository.GetByIdAsync(id);
        
        return getResult.IsSuccess
            ? Result<User>.Success(getResult.Data)
            : Result<User>.Failure(getResult.ErrorMessage!)!;
    }

    public async Task<Result> RegisterAsync(User userDto)
    {
        var hashPassword = new PasswordHasher<User>().HashPassword(userDto, userDto.Password);
        userDto.Password = hashPassword;
        
        var addResult = await userRepository.AddAsync(userDto);
        
        return addResult.IsSuccess
            ? Result.Success()
            : Result.Failure(addResult.ErrorMessage!);
    }

    public async Task<Result> DeleteUserAsync(Guid id)
    {
        var deleteResult = await userRepository.DeleteAsync(id);
        
        return deleteResult.IsSuccess
            ? Result.Success()
            : Result.Failure(deleteResult.ErrorMessage!);
    }

    public async Task<Result> AddFavoriteActorAsync(Guid userId, Guid actorId)
    {
        var addResult = await userRepository.AddFavoriteActorAsync(userId, actorId);

        return addResult.IsSuccess
            ? Result.Success()
            : Result.Failure(addResult.ErrorMessage!);
    }

    public async Task<Result> DeleteFavoriteActorAsync(Guid userId, Guid actorId)
    {
        var deleteResult = await userRepository.DeleteFavoriteActorAsync(userId, actorId);
        
        return deleteResult.IsSuccess
             ? Result.Success()
             : Result.Failure(deleteResult.ErrorMessage!);
    }

    public async Task<Result> AddToWatchListAsync(Guid userId, Guid movieId)
    {
        var addResult = await userRepository.AddToWatchListAsync(userId, movieId);
        
        return addResult.IsSuccess
            ? Result.Success()
            : Result.Failure(addResult.ErrorMessage!);
    }

    public async Task<Result> DeleteFromWatchListAsync(Guid userId, Guid movieId)
    {
        var deleteResult = await userRepository.DeleteFromWatchListAsync(userId, movieId);
        
        return deleteResult.IsSuccess
            ? Result.Success()
            : Result.Failure(deleteResult.ErrorMessage!);
    }

    public async Task<Result> AddPreferredGenresAsync(Guid userId, List<Genre> genres)
    {
        var addResult = await userRepository.AddPreferredGenresAsync(userId, genres);
        
        return addResult.IsSuccess
            ? Result.Success()
            : Result.Failure(addResult.ErrorMessage!);
    }

    public async Task<Result> AddOrUpdateAvatarAsync(string url, Guid userId)
    {
        var addOrUpdateResult = await userRepository.AddOrUpdateAvatarAsync(url, userId);
        
        return addOrUpdateResult.IsSuccess
            ? Result.Success()
            : Result.Failure(addOrUpdateResult.ErrorMessage!);
    }
}