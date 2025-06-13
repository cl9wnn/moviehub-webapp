using AutoMapper;
using Domain.Abstractions.Repositories;
using Domain.Dtos;
using Domain.Models;
using Domain.Utils;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

public class RecommendationRepository(AppDbContext dbContext, IMapper mapper) : IRecommendationRepository
{
    public async Task<Result<UserRecommendationDataDto>> GetUserWithRecommendationDataAsync(Guid userId)
    {
        var user = await dbContext.Users
            .Include(u => u.PreferredGenres)
            .Include(u => u.WatchList).ThenInclude(m => m.Genres)
            .Include(u => u.NotInterestedMovies)
            .Include(u => u.Topics).ThenInclude(t => t.Movie).ThenInclude(m => m.Genres)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            return Result<UserRecommendationDataDto>.Failure("User not found")!;
        };

        var genresFromUserTopics = user.Topics
            .SelectMany(t => t.Movie.Genres.Select(g => g.Id))
            .Distinct()
            .ToList();
        
        var recommendationData = new UserRecommendationDataDto
        {
            PreferredGenreIds = user.PreferredGenres.Select(g => g.Id).ToList(),
            WatchListGenreIds = user.WatchList.SelectMany(m => m.Genres.Select(g => g.Id)).Distinct().ToList(),
            NotInterestedMovieIds = user.NotInterestedMovies.Select(m => m.Id).ToList(),
            GenresFromUserTopics = genresFromUserTopics
        };
        
        return Result<UserRecommendationDataDto>.Success(recommendationData);
    }
    
    public async Task<Result<List<int>>> GetGenresFromLikedMoviesAsync(Guid userId)
    {
        var likedMovieIds = await dbContext.MovieRatings
            .Where(r => r.UserId == userId && r.Rating >= 8)
            .Select(r => r.MovieId)
            .Distinct()
            .ToListAsync();
        
        var likedGenres = await dbContext.Movies
            .Where(m => likedMovieIds.Contains(m.Id))
            .SelectMany(m => m.Genres.Select(g => g.Id))
            .Distinct()
            .ToListAsync();
        
        return Result<List<int>>.Success(likedGenres);
    }

    public async Task<Result<List<Guid>>> GetSeenMovieIdsAsync(Guid userId)
    {
        var seenMovies = await dbContext.MovieRatings
            .Where(r => r.UserId == userId)
            .Select(r => r.MovieId)
            .ToListAsync();
        
        return Result<List<Guid>>.Success(seenMovies);
    }
    
    public async Task<Result<List<Movie>>> GetCandidateMoviesAsync(List<Guid> seenMovieIds, List<Guid> notInterestedIds)
    {
        var recommendationCandidates = await dbContext.Movies
            .Include(m => m.Genres)
            .Where(m =>
                !m.IsDeleted &&
                !seenMovieIds.Contains(m.Id) &&
                !notInterestedIds.Contains(m.Id))
            .ToListAsync();
        
        var movies = mapper.Map<List<Movie>>(recommendationCandidates);
        
        return Result<List<Movie>>.Success(movies);
    }
    
    public async Task<Result<List<Movie>>> GetMovieRecommendationsByRatingAsync(int topN = 10)
    {
        var movieEntities = await dbContext.Movies
            .Where(m => m.IsDeleted == false)
            .OrderByDescending(r => r.RatingCount > 0 
                ? r.RatingSum/ r.RatingCount
                : 0.0)
            .AsNoTracking()
            .Take(topN)
            .ToListAsync();
        
        var movies = mapper.Map<List<Movie>>(movieEntities);
        
        return Result<List<Movie>>.Success(movies);
    }
}