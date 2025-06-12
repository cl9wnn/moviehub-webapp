using Domain.Models;
using Domain.Utils;

namespace Domain.Abstractions.Repositories;

public interface IRecommendationRepository
{
    Task<Result<List<Movie>>> GetMovieRecommendationsByUserAsync(Guid userId, int topN = 10);
    Task<Result<List<Movie>>> GetMovieRecommendationsByRatingAsync(int topN = 10);
}