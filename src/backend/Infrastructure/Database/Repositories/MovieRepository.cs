using AutoMapper;
using Domain.Abstractions.Repositories;
using Domain.Dtos;
using Domain.Models;
using Domain.Utils;
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
            .Include(m => m.Topics)
                .ThenInclude(t => t.User)
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
    
    public async Task<Result> ExistsAsync(Guid id)
    {
        var exists = await dbContext.Movies
            .AsNoTracking()
            .AnyAsync(c => c.Id == id && !c.IsDeleted);

        return exists
            ? Result.Success()
            : Result.Failure("Movie not found!");
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
        
        var movies = mapper.Map<ICollection<Movie>>(movieEntities);
        
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

    public async Task<Result> AddOrUpdatePosterPhotoAsync(string url, Guid id)
    {
        var movieEntity = await ActiveMovies
            .FirstOrDefaultAsync(a => a.Id == id);

        if (movieEntity == null)
        {
            return Result.Failure("Movie not found")!;
        }
        
        movieEntity.PosterUrl = url;
        await dbContext.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<Result> AddMoviePhotoAsync(Photo photo, Guid id)
    {
        var moviePhotoEntity = mapper.Map<MoviePhotoEntity>(photo);
        moviePhotoEntity.MovieId = id;
        
        await dbContext.MoviePhotos.AddAsync(moviePhotoEntity);
        await dbContext.SaveChangesAsync();
        
        return Result.Success();
    }

    public async Task<Result> RateMovieAsync(Guid id, Guid userId, int rating)
    {
        var movie = await ActiveMovies
            .FirstOrDefaultAsync(a => a.Id == id);

        if (movie == null)
        {
            return Result.Failure("Movie not found")!;
        }

        await using var transaction = await dbContext.Database.BeginTransactionAsync();

        try
        {
            var existingRating = await dbContext.MovieRatings
                .FirstOrDefaultAsync(m => m.MovieId == id && m.UserId == userId);

            if (existingRating == null)
            {
                var movieRatingEntity = new MovieRatingEntity
                {
                    MovieId = id,
                    UserId = userId,
                    Rating = rating
                };
                
                dbContext.MovieRatings.Add(movieRatingEntity);
                
                movie.RatingCount++;
                movie.RatingSum += rating;
            }
            else
            {
                movie.RatingSum = movie.RatingSum - existingRating.Rating + rating;
                existingRating.Rating = rating;
            }
            
            await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
            
            return Result.Success();
        }
        catch
        {
            await transaction.RollbackAsync();
            return Result.Failure("Rating failed!")!;
        }
    }

    public async Task<Result<List<DiscussionTopic>>> GetTopicsByMovieIdAsync(Guid movieId)
    {
        var topicEntities = await dbContext.DiscussionTopics
            .Where(t => t.MovieId == movieId && !t.IsDeleted)
            .Include(t => t.Movie)
            .Include(t => t.Tags)
            .AsNoTracking()
            .ToListAsync();

        var topics = mapper.Map<List<DiscussionTopic>>(topicEntities);
        
        return Result<List<DiscussionTopic>>.Success(topics);
    }
}