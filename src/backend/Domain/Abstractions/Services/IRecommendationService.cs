using Domain.Models;
using Domain.Utils;

namespace Domain.Abstractions.Services;

public interface IRecommendationService
{
    Task<Result<List<Movie>>> GetMovieRecommendationsByUserAsync(Guid userId);
    Task<Result<List<Movie>>> GetMovieRecommendationsByRatingAsync();
}