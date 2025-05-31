using AutoMapper;
using Domain.Abstractions.Repositories;
using Domain.Models;
using Domain.Utils;
using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Database.Repositories;

public class ActorRepository(AppDbContext dbContext, IMapper mapper): IActorRepository
{
    private IQueryable<ActorEntity> ActiveActors => dbContext.Actors.Where(a => !a.IsDeleted);

    public async Task<Result<Actor>> GetByIdAsync(Guid id)
    {
        var actorEntity = await ActiveActors
            .Include(a => a.Photos)
            .Include(a => a.MovieActors)
                .ThenInclude(a => a.Movie)
            .FirstOrDefaultAsync(a => a.Id == id);
        
        if (actorEntity == null)
        {
            return Result<Actor>.Failure("Actor not found!")!;
        }
        
        var actor =  mapper.Map<Actor>(actorEntity);
        
        return Result<Actor>.Success(actor);
    }

    public async Task<Result<Actor>> AddAsync(Actor actorDto)
    {
        var actorEntity = mapper.Map<ActorEntity>(actorDto);
        
        await dbContext.Actors.AddAsync(actorEntity);
        await dbContext.SaveChangesAsync();
        
        var actor = mapper.Map<Actor>(actorEntity);
        
        return Result<Actor>.Success(actor);
    }

    public async Task<Result<Actor>> UpdateAsync(Actor actorDto)
    {
        var existingActor = await ActiveActors
            .FirstOrDefaultAsync(a => a.Id == actorDto.Id);

        if (existingActor == null)
        {
            return Result<Actor>.Failure("Actor not found")!;
        }

        mapper.Map(actorDto, existingActor);
        await dbContext.SaveChangesAsync();
        
        var updated = mapper.Map<Actor>(existingActor);
        
        return Result<Actor>.Success(updated);
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var existingActor = await ActiveActors
            .FirstOrDefaultAsync(a => a.Id == id);
        
        if (existingActor == null)
        {
            return Result.Failure("Actor not found")!;
        }
        
        existingActor!.IsDeleted = true;
        await dbContext.SaveChangesAsync();
        
        return Result.Success();
    }

    public async Task<Result<ICollection<Actor>>> GetAllAsync()
    {
        var actorEntities = await ActiveActors
            .Include(a => a.Photos)
            .Include(a => a.MovieActors)
                .ThenInclude(a => a.Movie)
            .AsNoTracking()
            .ToListAsync();
        
        var actors = mapper.Map<List<Actor>>(actorEntities);

        return Result<ICollection<Actor>>.Success(actors);
    }

    public async Task<Result> AddOrUpdatePortraitAsync(string url, Guid id)
    {
        var actorEntity = await ActiveActors
            .FirstOrDefaultAsync(a => a.Id == id);

        if (actorEntity == null)
        {
            return Result.Failure("Actor not found")!;
        }
        
        actorEntity.PhotoUrl = url;
        await dbContext.SaveChangesAsync();
        
        return Result.Success();
    }

    public async Task<Result> AddActorPhotoAsync(Photo photo, Guid id)
    {
        var actorPhotoEntity = mapper.Map<ActorPhotoEntity>(photo);
        actorPhotoEntity.ActorId = id;
        
        await dbContext.ActorPhotos.AddAsync(actorPhotoEntity);
        await dbContext.SaveChangesAsync();
        
        return Result.Success();
    }
}