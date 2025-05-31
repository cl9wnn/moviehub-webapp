using Domain.Models;
using Domain.Utils;

namespace Domain.Abstractions.Repositories;

public interface IUserRepository: IRepository<Guid, User>
{ 
    Task<Result<User>> GetByUsernameAsync(string username);
    Task<Result> AddPreferredGenresAsync(Guid userId, List<Genre> genres);
    Task<Result> AddFavoriteActorAsync(Guid userId, Guid actorId);
    Task<Result> DeleteFavoriteActorAsync(Guid userId, Guid actorId);
    Task<Result> AddToWatchListAsync(Guid userId, Guid movieId);
    Task<Result> DeleteFromWatchListAsync(Guid userId, Guid movieId);
}