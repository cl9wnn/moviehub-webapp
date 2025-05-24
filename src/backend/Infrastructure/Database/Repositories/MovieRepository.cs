using Application.Utils;
using AutoMapper;
using Domain.Abstractions.Repositories;
using Domain.Dtos;
using Domain.Models;
using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Database.Repositories;

public class MovieRepository(AppDbContext dbContext, IMapper mapper): IMovieRepository
{
    private IQueryable<MovieEntity> ActiveMovies => dbContext.Movies.Where(a => !a.IsDeleted);

    public async Task<Result<Movie?>> GetByIdAsync(Guid id)
    {
        var movieEntity = await ActiveMovies
            .Include(m => m.Directors)
            .Include(m => m.Writers)
            .Include(m => m.Genres)
            .Include(m => m.Photos)
            .Include(a => a.MovieActors)
                .ThenInclude(a => a.Actor)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (movieEntity == null)
        {
            return Result<Movie?>.Failure("Movie not found!");
        }
        
        var movie =  mapper.Map<Movie?>(movieEntity);
        return Result<Movie?>.Success(movie);
    }

    public async Task<Result<Movie>> AddAsync(Movie movieDto)
    {
            var genreIds = movieDto.Genres.Select(g => g.Id).ToList();
            
            var genreEntities = await dbContext.Genres
                .Where(g => genreIds.Contains(g.Id))
                .ToListAsync();
            
            var missingIds = genreIds.Except(genreEntities.Select(g => g.Id)).ToList();
            
            if (missingIds.Count != 0)
            {
                return Result<Movie>.Failure("Genres not found")!;
            }

            var movieEntity = mapper.Map<MovieEntity>(movieDto);
            
            movieEntity.Genres = genreEntities;
            
            await dbContext.Movies.AddAsync(movieEntity);
            await dbContext.SaveChangesAsync();

            var movie = mapper.Map<Movie>(movieEntity);
            return Result<Movie>.Success(movie);
    }

    public async Task<Result<Movie>> UpdateAsync(Movie movieDto)
    {
        var existingMovie = await ActiveMovies
            .FirstOrDefaultAsync(a => a.Id == movieDto.Id);

        if (existingMovie == null)
        {
            return Result<Movie>.Failure("Movie not found")!;
        }

        mapper.Map(movieDto, existingMovie);
        await dbContext.SaveChangesAsync();
        
        var updated = mapper.Map<Movie>(existingMovie);
        return Result<Movie>.Success(updated);
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var existingMovie = await ActiveMovies
            .FirstOrDefaultAsync(a => a.Id == id);
        
        if (existingMovie == null)
        {
            return Result.Failure("Movie not found")!;
        }
        
        existingMovie!.IsDeleted = true;
        await dbContext.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<Result<ICollection<Movie>>> GetAllAsync()
    {
        var movieEntities = await ActiveMovies
            .Include(m => m.Directors)
            .Include(m => m.Writers)
            .Include(m => m.Genres)
            .Include(m => m.Photos)
            .Include(a => a.MovieActors)
            .ThenInclude(a => a.Actor)
            .AsNoTracking()
            .ToListAsync();
        
        var movies = mapper.Map<List<Movie>>(movieEntities);
        return Result<ICollection<Movie>>.Success(movies);
    }

    public async Task<Result> AddActorsAsync(List<MovieActorDto> actors)
    {
        var actorEntities = mapper.Map<List<MovieActorEntity>>(actors);
        
        var actorIds = actorEntities.Select(a => a.ActorId).Distinct().ToList();
        var movieId = actorEntities.FirstOrDefault()!.MovieId;
        
        var existingActorIds = await dbContext.Actors
            .Where(a => actorIds.Contains(a.Id))
            .Select(a => a.Id)
            .ToListAsync();
        
        var missingActorIds = actorIds.Except(existingActorIds).ToList();
        
        if (missingActorIds.Count != 0)
        {
            return Result.Failure($"Next actor id's not found: {string.Join(", ", missingActorIds)}");
        }
        
        var existingMovieActorIds = await dbContext.MovieActors
            .Where(ma => ma.MovieId == movieId && actorIds.Contains(ma.ActorId))
            .Select(ma => ma.ActorId)
            .ToListAsync();

        if (existingMovieActorIds.Count != 0)
        {
            return Result.Failure($"Next actor id's already exist: {string.Join(", ", existingMovieActorIds)}");
        }
        
        await dbContext.MovieActors.AddRangeAsync(actorEntities);
        await dbContext.SaveChangesAsync();
        
        return Result.Success();
    }
}