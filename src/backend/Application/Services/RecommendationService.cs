using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
using Domain.Models;
using Domain.Utils;

namespace Application.Services;

public class RecommendationService(IRecommendationRepository recommendationRepository, IDistributedCacheService cacheService): IRecommendationService
{
    private const string MoviesCacheKey = "top-movies:all";
    public async Task<Result<List<Movie>>> GetMovieRecommendationsByUserAsync(Guid userId, int topN = 10)
    {
        var userDataResult = await recommendationRepository.GetUserWithRecommendationDataAsync(userId);

        if (!userDataResult.IsSuccess)
        {
            return Result<List<Movie>>.Failure(userDataResult.ErrorMessage!)!;
        }

        var userData = userDataResult.Data;
        
        var likedMovieGenresResult = await recommendationRepository.GetGenresFromLikedMoviesAsync(userId);
        var seenMovieIdsResult = await recommendationRepository.GetSeenMovieIdsAsync(userId);

        var genreWeights = new Dictionary<int, double>();
        
        AddWeight(genreWeights, userData.PreferredGenreIds, 1.0);
        AddWeight(genreWeights, userData.GenresFromUserTopics, 0.9);
        AddWeight(genreWeights, likedMovieGenresResult.Data, 0.7);
        AddWeight(genreWeights, userData.WatchListGenreIds, 0.5);

        var candidatesResult = await recommendationRepository.GetCandidateMoviesAsync(seenMovieIdsResult.Data, userData.NotInterestedMovieIds);

        var scored = candidatesResult.Data
            .Select(m =>
            {
                var score = m.Genres.Sum(g => genreWeights.GetValueOrDefault(g.Id, 0));
                return new { Movie = m, Score = score };
            })
            .Where(x => x.Score > 0)
            .OrderByDescending(x => x.Score)
            .ThenByDescending(x =>
                x.Movie.RatingCount > 0 ? x.Movie.RatingSum / x.Movie.RatingCount : 0.0)
            .Take(topN)
            .Select(x => x.Movie)
            .ToList();

        return Result<List<Movie>>.Success(scored);
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
    
    private void AddWeight(Dictionary<int, double> weights, IEnumerable<int> genreIds, double weight)
    {
        foreach (var gid in genreIds)
        {
            if (weights.TryGetValue(gid, out var current))
                weights[gid] = current + weight;
            else
                weights[gid] = weight;
        }
    }
}