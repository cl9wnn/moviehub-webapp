using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
using Domain.Dtos;
using Domain.Models;
using Domain.Utils;

namespace Application.Services;

public class MovieService(IMovieRepository movieRepository, IUserRepository userRepository,
    IDistributedCacheService cacheService): IMovieService
{
    private const string MoviesCacheKey = "movies:all";
    public async Task<Result<List<Movie>>> GetAllMoviesAsync()
    {
        var cachedMovies = await cacheService.GetAsync<List<Movie>>(MoviesCacheKey);
        
        if (cachedMovies != null)
        {
            return Result<List<Movie>>.Success(cachedMovies);
        }
        
        var getResult = await movieRepository.GetAllAsync();

        if (!getResult.IsSuccess)
        {
            return Result<List<Movie>>.Failure(getResult.ErrorMessage!)!;
        }
        
        var movies = getResult.Data;
        await cacheService.SetAsync(MoviesCacheKey, movies, TimeSpan.FromMinutes(10));
        
        return Result<List<Movie>>.Success(getResult.Data.ToList());
    }

    public async Task<Result<Movie>> GetByIdAsync(Guid id)
    {
        var getResult = await movieRepository.GetByIdAsync(id);
        
        return getResult.IsSuccess
            ? Result<Movie>.Success(getResult.Data)
            : Result<Movie>.Failure(getResult.ErrorMessage!)!;
    }
    
    public async Task<Result> ExistsAsync(Guid id)
    {
        var existsResult = await movieRepository.ExistsAsync(id);
        
        return existsResult.IsSuccess
            ? Result.Success()
            : Result.Failure(existsResult.ErrorMessage!)!;
    }

    public async Task<Result<Movie>> CreateMovieAsync(Movie movie)
    {
        var createResult = await movieRepository.AddAsync(movie); 
    
        if (createResult.IsSuccess)
        {
            await cacheService.RemoveAsync(MoviesCacheKey);
            return Result<Movie>.Success(createResult.Data);
        }

        return Result<Movie>.Failure(createResult.ErrorMessage!)!;
    }

    public async Task<Result> DeleteMovieAsync(Guid id)
    {
        var deleteResult = await movieRepository.DeleteAsync(id);

        if (deleteResult.IsSuccess)
        {
            await cacheService.RemoveAsync(MoviesCacheKey);
            return Result.Success();
        }
      
        return Result.Failure(deleteResult.ErrorMessage!);
    }

    public async Task<Result> AddActorsAsync(List<MovieActorDto> actors)
    {
        if (actors.Count == 0)
        {
            return Result.Failure("Actors cannot be empty.");
        }
        
        var addResult = await movieRepository.AddActorsAsync(actors);
        
        return  addResult.IsSuccess
            ? Result.Success()
            : Result.Failure(addResult.ErrorMessage!);
    }

    public async Task<Result> AddOrUpdatePosterPhotoAsync(string url, Guid id)
    {
        var addOrUpdateResult = await movieRepository.AddOrUpdatePosterPhotoAsync(url, id);
        
        return addOrUpdateResult.IsSuccess
            ? Result.Success()
            : Result.Failure(addOrUpdateResult.ErrorMessage!);
    }

    public async Task<Result> AddMoviePhotoAsync(Photo photo, Guid id)
    {
        var addResult = await movieRepository.AddMoviePhotoAsync(photo, id);
        
        return addResult.IsSuccess
            ? Result.Success()
            : Result.Failure(addResult.ErrorMessage!);
    }

    public async Task<Result<MovieWithUserInfoDto>> GetMovieWithUserInfoAsync(Guid userId, Guid movieId)
    {
        var getResult  = await movieRepository.GetByIdAsync(movieId);

        if (!getResult.IsSuccess)
        {
            return Result<MovieWithUserInfoDto>.Failure(getResult.ErrorMessage!)!;
        }
        
        var movie = getResult.Data;
        
        var isInWatchListResult = await userRepository.IsMovieInWatchListAsync(userId, movieId);

        if (!isInWatchListResult.IsSuccess)
        {
            return Result<MovieWithUserInfoDto>.Failure(isInWatchListResult.ErrorMessage!)!;
        }
        
        var ratingResult = await userRepository.GetMovieRatingAsync(userId, movieId);

        if (!ratingResult.IsSuccess)
        {
            return Result<MovieWithUserInfoDto>.Failure(ratingResult.ErrorMessage!)!;
        }
        
        var actorWithInfo = new MovieWithUserInfoDto
        {
            Movie = movie,
            IsInWatchList = isInWatchListResult.Data,
            OwnRating = ratingResult.Data
        };
        
        return Result<MovieWithUserInfoDto>.Success(actorWithInfo);
    }

    public async Task<Result> RateMovieAsync(Guid id, Guid userId, int rating)
    {
        var rateResult = await movieRepository.RateMovieAsync(id, userId, rating);
        
        return rateResult.IsSuccess
            ? Result.Success()
            : Result.Failure(rateResult.ErrorMessage!);
    }

    public async Task<Result<List<DiscussionTopic>>> GetTopicsByMovieIdAsync(Guid movieId)
    {
        var getResult = await movieRepository.GetTopicsByMovieIdAsync(movieId);
        
        return getResult.IsSuccess
            ? Result<List<DiscussionTopic>>.Success(getResult.Data.ToList())
            : Result<List<DiscussionTopic>>.Failure(getResult.ErrorMessage!)!;
    }
}