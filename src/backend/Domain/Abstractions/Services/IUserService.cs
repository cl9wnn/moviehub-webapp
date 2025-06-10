using Domain.Dtos;
using Domain.Models;
using Domain.Utils;

namespace Domain.Abstractions.Services;

public interface IUserService: IEntityService<User>
{
    Task<Result<List<User>>> GetAllUsersAsync();
    Task<Result<AuthDto>> RegisterAsync(User user);
    Task<Result> DeleteUserAsync(Guid id);
    Task<Result> AddFavoriteActorAsync(Guid userId, Guid actorId);
    Task<Result> DeleteFavoriteActorAsync(Guid userId, Guid actorId);
    Task<Result> AddToWatchListAsync(Guid userId, Guid movieId);
    Task<Result> DeleteFromWatchListAsync(Guid userId, Guid movieId);
    Task<Result> PersonalizeUserAsync(PersonalizeUserDto personalizeUserDto, Guid userId);
    Task<Result> AddOrUpdateAvatarAsync(string url, Guid userId);
    Task<Result<List<Comment>>> GetCommentsByUserIdAsync(Guid id);
    Task<Result<List<DiscussionTopic>>> GetTopicsByUserIdAsync(Guid id);
}