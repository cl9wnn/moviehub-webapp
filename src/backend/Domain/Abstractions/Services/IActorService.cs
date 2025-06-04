using Domain.Dtos;
using Domain.Models;
using Domain.Utils;

namespace Domain.Abstractions.Services;

public interface IActorService: IEntityService<Actor>
{
    Task<Result<List<Actor>>> GetAllActorsAsync();
    Task<Result<Actor>> CreateActorAsync(Actor actor);
    Task<Result> DeleteActorAsync(Guid id);
    Task<Result> AddOrUpdatePortraitPhotoAsync(string url, Guid id);
    Task<Result> AddActorPhotoAsync(Photo photo, Guid id);
    Task<Result<ActorWithUserInfoDto>> GetActorWithUserInfoAsync(Guid userId, Guid actorId);

}