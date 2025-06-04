using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
using Domain.Dtos;
using Domain.Models;
using Domain.Utils;

namespace Application.Services;

public class ActorService(IActorRepository actorRepository, IUserRepository userRepository): IActorService
{
    public async Task<Result<List<Actor>>> GetAllActorsAsync()
    {
        var getResult = await actorRepository.GetAllAsync();
        
        return Result<List<Actor>>.Success(getResult.Data.ToList());
    }

    public async Task<Result<Actor>> GetByIdAsync(Guid id)
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

    public async Task<Result> AddOrUpdatePortraitPhotoAsync(string url, Guid id)
    {
        var addOrUpdateResult = await actorRepository.AddOrUpdatePortraitAsync(url, id);
        
        return addOrUpdateResult.IsSuccess
            ? Result.Success()
            : Result.Failure(addOrUpdateResult.ErrorMessage!);
    }

    public async Task<Result> AddActorPhotoAsync(Photo photo, Guid id)
    {
        var addResult = await actorRepository.AddActorPhotoAsync(photo, id);
        
        return addResult.IsSuccess
            ? Result.Success()
            : Result.Failure(addResult.ErrorMessage!);
    }

    public async Task<Result<ActorWithUserInfoDto>> GetActorWithUserInfoAsync(Guid userId, Guid actorId)
    {
        var getResult  = await actorRepository.GetByIdAsync(actorId);

        if (!getResult.IsSuccess)
        {
            return Result<ActorWithUserInfoDto>.Failure(getResult.ErrorMessage!)!;
        }
        
        var actor = getResult.Data;
        
        var isFavoriteResult = await userRepository.IsActorFavoriteAsync(userId, actorId);

        if (!isFavoriteResult.IsSuccess)
        {
            return Result<ActorWithUserInfoDto>.Failure(isFavoriteResult.ErrorMessage!)!;
        }

        var actorWithInfo = new ActorWithUserInfoDto
        {
            Actor = actor,
            IsFavorite = isFavoriteResult.Data,
        };
        
        return Result<ActorWithUserInfoDto>.Success(actorWithInfo);
    }
}