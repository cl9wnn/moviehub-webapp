using Application.Utils;
using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ActorService(IActorRepository actorRepository): IActorService
{
    public async Task<Result<List<Actor>>> GetAllActorsAsync()
    {
        var getResult = await actorRepository.GetAllAsync();
        
        return Result<List<Actor>>.Success(getResult.Data.ToList());
    }

    public async Task<Result<Actor>> GetActorAsync(Guid id)
    {
        var getResult = await actorRepository.GetByIdAsync(id);
        
        return getResult.IsSuccess
            ? Result<Actor>.Success(getResult.Data)
            : Result<Actor>.Failure(getResult.ErrorMessage!)!;
    }

    public async Task<Result<Actor>> CreateActorAsync(Actor movie)
    {
        var createResult = await actorRepository.AddAsync(movie);
        
        return Result<Actor>.Success(createResult.Data);
    }

    public async Task<Result> DeleteActorAsync(Guid id)
    {
        var deleteResult = await actorRepository.DeleteAsync(id);
        
        return deleteResult.IsSuccess
            ? Result.Success()
            : Result.Failure(deleteResult.ErrorMessage!);
    }
}