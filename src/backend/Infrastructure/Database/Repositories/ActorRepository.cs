using Application.Utils;
using AutoMapper;
using Domain.Abstractions.Repositories;
using Domain.Models;
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

    public async Task<Result<Actor>> AddAsync(Actor entity)
    {
        var actorEntity = mapper.Map<ActorEntity>(entity);
        
        await dbContext.Actors.AddAsync(actorEntity);
        await dbContext.SaveChangesAsync();
        
        var actor = mapper.Map<Actor>(actorEntity);
        return Result<Actor>.Success(actor);
    }

    public async Task<Result<Actor>> UpdateAsync(Actor entity)
    {
        var existingActor = await ActiveActors
            .FirstOrDefaultAsync(a => a.Id == entity.Id);

        if (existingActor == null)
        {
            return Result<Actor>.Failure("Actor not found")!;
        }

        mapper.Map(entity, existingActor);
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
}