using Domain.Models;
using Domain.Utils;

namespace Domain.Abstractions.Services;

public interface IRecommendationService
{
    Task<Result<List<Movie>>> GetMovieRecommendationsByUserAsync(Guid userId, int topN = 10);
    Task<Result<List<Movie>>> GetMovieRecommendationsByRatingAsync();
}