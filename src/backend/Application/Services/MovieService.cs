using Application.Utils;
using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
using Domain.Dtos;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class MovieService(IMovieRepository movieRepository): IMovieService
{
    public async Task<Result<List<Movie>>> GetAllMoviesAsync()
    {
        var getResult = await movieRepository.GetAllAsync();
        return Result<List<Movie>>.Success(getResult.Data.ToList());
    }

    public async Task<Result<Movie>> GetMovieAsync(Guid id)
    {
        var getResult = await movieRepository.GetByIdAsync(id);
        
        return getResult.IsSuccess
            ? Result<Movie>.Success(getResult.Data)
            : Result<Movie>.Failure(getResult.ErrorMessage!)!;
    }

    public async Task<Result<Movie>> CreateMovieAsync(Movie movie)
    {
        var createResult = await movieRepository.AddAsync(movie); 
        
        return createResult.IsSuccess
            ? Result<Movie>.Success(createResult.Data)
            : Result<Movie>.Failure(createResult.ErrorMessage!)!;
    }

    public async Task<Result> DeleteMovieAsync(Guid id)
    {
        var deleteResult = await movieRepository.DeleteAsync(id);
        
        return deleteResult.IsSuccess
            ? Result.Success()
            : Result.Failure(deleteResult.ErrorMessage!);
    }

    public async Task<Result> AddActorsAsync(List<MovieActorDto> actors)
    {
        if (actors.Count == 0)
        {
            return Result.Failure("Actors cannot be empty.");
        }
        
        var addResult = await movieRepository.AddActorsAsync(actors);
        
        return  addResult.IsSuccess
            ? Result.Success()
            : Result.Failure(addResult.ErrorMessage!);
    }
}