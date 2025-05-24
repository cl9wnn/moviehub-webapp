using Application.Utils;
using Domain.Models;

namespace Domain.Abstractions.Services;

public interface IActorService
{
    Task<Result<List<Actor>>> GetAllActorsAsync();
    Task<Result<Actor>> GetActorAsync(Guid id);
    Task<Result<Actor>> CreateActorAsync(Actor actor);
    Task<Result> DeleteActorAsync(Guid id);
}