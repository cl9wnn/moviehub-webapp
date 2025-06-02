using Domain.Models;
using Domain.Utils;

namespace Domain.Abstractions.Services;

public interface IUserService
{
    Task<Result<List<User>>> GetAllUsersAsync();
    Task<Result<User>> GetUserAsync(Guid id);
    Task<Result> RegisterAsync(User user);
    Task<Result> DeleteUserAsync(Guid id);
    Task<Result> AddFavoriteActorAsync(Guid userId, Guid actorId);
    Task<Result> DeleteFavoriteActorAsync(Guid userId, Guid actorId);
    Task<Result> AddToWatchListAsync(Guid userId, Guid movieId);
    Task<Result> DeleteFromWatchListAsync(Guid userId, Guid movieId);
    Task<Result> AddPreferredGenresAsync(Guid userId, List<Genre> genres);
    Task<Result> AddOrUpdateAvatarAsync(string url, Guid userId);
}