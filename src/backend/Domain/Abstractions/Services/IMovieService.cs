using Application.Utils;
using Domain.Dtos;
using Domain.Models;

namespace Domain.Abstractions.Services;

public interface IMovieService
{
    Task<Result<List<Movie>>> GetAllMoviesAsync();
    Task<Result<Movie>> GetMovieAsync(Guid id);
    Task<Result<Movie>> CreateMovieAsync(Movie actor);
    Task<Result> DeleteMovieAsync(Guid id);
    Task<Result> AddActorsAsync(List<MovieActorDto> actors);
}