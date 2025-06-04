using Domain.Dtos;
using Domain.Models;
using Domain.Utils;

namespace Domain.Abstractions.Services;

public interface IMovieService: IEntityService<Movie>
{
    Task<Result<List<Movie>>> GetAllMoviesAsync();
    Task<Result<Movie>> CreateMovieAsync(Movie actor);
    Task<Result> DeleteMovieAsync(Guid id);
    Task<Result> AddActorsAsync(List<MovieActorDto> actors);
    Task<Result> AddOrUpdatePosterPhotoAsync(string url, Guid id);
    Task<Result> AddMoviePhotoAsync(Photo photo, Guid id);
    Task<Result<MovieWithUserInfoDto>> GetMovieWithUserInfoAsync(Guid userId, Guid movieId);
}