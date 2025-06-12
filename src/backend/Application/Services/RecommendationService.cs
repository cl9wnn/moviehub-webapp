using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
using Domain.Models;
using Domain.Utils;

namespace Application.Services;

public class RecommendationService(IRecommendationRepository recommendationRepository, IDistributedCacheService cacheService): IRecommendationService
{
    private const string MoviesCacheKey = "top-movies:all";
    public async Task<Result<List<Movie>>> GetMovieRecommendationsByUserAsync(Guid userId)
    {
        var getResult = await recommendationRepository.GetMovieRecommendationsByUserAsync(userId);
        
        return getResult.IsSuccess
            ? Result<List<Movie>>.Success(getResult.Data)
            : Result<List<Movie>>.Failure(getResult.ErrorMessage!)!;
    }

    public async Task<Result<List<Movie>>> GetMovieRecommendationsByRatingAsync()
    {
        var cachedMovies = await cacheService.GetAsync<List<Movie>>(MoviesCacheKey);

        if (cachedMovies != null)
        {
            return Result<List<Movie>>.Success(cachedMovies);
        }
        
        var getResult = await recommendationRepository.GetMovieRecommendationsByRatingAsync();
        
        if (!getResult.IsSuccess)
        {
            return Result<List<Movie>>.Failure(getResult.ErrorMessage!)!;
        }
        
        var movies = getResult.Data;
        await cacheService.SetAsync(MoviesCacheKey, movies, TimeSpan.FromMinutes(10));
        
        return Result<List<Movie>>.Success(getResult.Data);
    }
}