using Domain.Models;
using Domain.Utils;

namespace Domain.Abstractions.Repositories;

public interface IActorRepository: IRepository<Guid, Actor>
{
    Task<Result> AddOrUpdatePortraitAsync(string url, Guid id);
    Task<Result> AddActorPhotoAsync(Photo photo, Guid id);
}