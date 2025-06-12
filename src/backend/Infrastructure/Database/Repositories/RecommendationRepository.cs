using AutoMapper;
using Domain.Abstractions.Repositories;
using Domain.Models;
using Domain.Utils;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

public class RecommendationRepository(AppDbContext dbContext, IMapper mapper) : IRecommendationRepository
{
    public async Task<Result<List<Movie>>> GetMovieRecommendationsByUserAsync(Guid userId, int topN = 10)
    {
        var user = await dbContext.Users
            .Include(u => u.PreferredGenres)
            .Include(u => u.WatchList)
                .ThenInclude(m => m.Genres)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            return Result<List<Movie>>.Failure("User not found")!;

        // 1. Получаем жанры пользователя
        var preferredGenreIds = user.PreferredGenres.Select(g => g.Id).ToList();

        // 2. Получаем ID понравившихся фильмов
        var likedMovieIds = await dbContext.MovieRatings
            .Where(r => r.UserId == userId && r.Rating >= 7)
            .Select(r => r.MovieId)
            .Distinct()
            .ToListAsync();

        // 3. Получаем жанры из понравившихся фильмов
        var likedGenreIds = await dbContext.Movies
            .Where(m => likedMovieIds.Contains(m.Id))
            .SelectMany(m => m.Genres.Select(g => g.Id))
            .Distinct()
            .ToListAsync();

        // 4. Получаем жанры из WatchList
        var watchListGenreIds = user.WatchList
            .SelectMany(m => m.Genres.Select(g => g.Id))
            .Distinct()
            .ToList();

        // 5. Подсчитываем веса жанров
        var genreWeights = new Dictionary<int, double>();
        foreach (var gid in preferredGenreIds)
            AddWeight(genreWeights, gid, 1.0);
        foreach (var gid in likedGenreIds)
            AddWeight(genreWeights, gid, 0.7);
        foreach (var gid in watchListGenreIds)
            AddWeight(genreWeights, gid, 0.5);

        // 6. Фильмы, которые пользователь уже оценивал
        var seenMovieIds = await dbContext.MovieRatings
            .Where(r => r.UserId == userId)
            .Select(r => r.MovieId)
            .ToListAsync();

        // ------------------ Фильмы которые пользователь пометил как "Не интересно" --------------
        
        
        // 7. Кандидаты на рекомендацию (не удалены и не видны)
        var candidates = await dbContext.Movies
            .Include(m => m.Genres)
            .Where(m => !m.IsDeleted && !seenMovieIds.Contains(m.Id))
            .ToListAsync();

        // 8. Расчёт score и ранжирование
        var scored = candidates
            .Select(m =>
            {
                double score = m.Genres.Sum(g => genreWeights.GetValueOrDefault(g.Id, 0));
                return new { Movie = m, Score = score };
            })
            .Where(x => x.Score > 0)
            .OrderByDescending(x => x.Score)
            .ThenByDescending(x =>
                x.Movie.RatingCount > 0
                    ? x.Movie.RatingSum / x.Movie.RatingCount
                    : 0.0)
            .Take(topN)
            .Select(x => x.Movie)
            .ToList();

        var movies = mapper.Map<List<Movie>>(scored);
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

    private void AddWeight(Dictionary<int, double> weights, int genreId, double weight)
    {
        if (weights.TryGetValue(genreId, out var current))
            weights[genreId] = current + weight;
        else
            weights[genreId] = weight;
    }
}