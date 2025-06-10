using Domain.Dtos;
using Domain.Models;
using Domain.Utils;

namespace Domain.Abstractions.Repositories;

public interface IUserRepository: IRepository<Guid, User>
{ 
    Task<Result<User>> GetByUsernameAsync(string username);
    Task<Result> AddFavoriteActorAsync(Guid userId, Guid actorId);
    Task<Result> DeleteFavoriteActorAsync(Guid userId, Guid actorId);
    Task<Result> AddToWatchListAsync(Guid userId, Guid movieId);
    Task<Result> DeleteFromWatchListAsync(Guid userId, Guid movieId);
    Task<Result<bool>> IsActorFavoriteAsync(Guid userId, Guid actorId);
    Task<Result<bool>> IsMovieInWatchListAsync(Guid userId, Guid movieId);
    Task<Result> AddOrUpdateAvatarAsync(string url, Guid userId);
    Task<Result> PersonalizeUserAsync(PersonalizeUserDto personalizeUserDto, Guid userId);
    Task<Result<int?>> GetMovieRatingAsync(Guid userId, Guid movieId);
    Task<Result<List<Comment>>>GetCommentsByUserIdAsync(Guid userId);
    Task<Result<List<DiscussionTopic>>> GetTopicsByUserIdAsync(Guid userId);
}