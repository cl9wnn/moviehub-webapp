using Domain.Dtos;
using Domain.Models;
using Domain.Utils;

namespace Domain.Abstractions.Repositories;

public interface IRecommendationRepository
{
    Task<Result<List<Movie>>> GetMovieRecommendationsByRatingAsync(int topN = 10);
    Task<Result<UserRecommendationDataDto>> GetUserWithRecommendationDataAsync(Guid userId);
    Task<Result<List<int>>> GetGenresFromLikedMoviesAsync(Guid userId);
    Task<Result<List<Guid>>> GetSeenMovieIdsAsync(Guid userId);
    Task<Result<List<Movie>>> GetCandidateMoviesAsync(List<Guid> seenMovieIds, List<Guid> notInterestedIds);
}