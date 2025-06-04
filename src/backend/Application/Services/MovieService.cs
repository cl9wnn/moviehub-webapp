using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
using Domain.Dtos;
using Domain.Models;
using Domain.Utils;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class MovieService(IMovieRepository movieRepository, IUserRepository userRepository): IMovieService
{
    public async Task<Result<List<Movie>>> GetAllMoviesAsync()
    {
        var getResult = await movieRepository.GetAllAsync();
        return Result<List<Movie>>.Success(getResult.Data.ToList());
    }

    public async Task<Result<Movie>> GetByIdAsync(Guid id)
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

    public async Task<Result> AddOrUpdatePosterPhotoAsync(string url, Guid id)
    {
        var addOrUpdateResult = await movieRepository.AddOrUpdatePosterPhotoAsync(url, id);
        
        return addOrUpdateResult.IsSuccess
            ? Result.Success()
            : Result.Failure(addOrUpdateResult.ErrorMessage!);
    }

    public async Task<Result> AddMoviePhotoAsync(Photo photo, Guid id)
    {
        var addResult = await movieRepository.AddMoviePhotoAsync(photo, id);
        
        return addResult.IsSuccess
            ? Result.Success()
            : Result.Failure(addResult.ErrorMessage!);
    }

    public async Task<Result<MovieWithUserInfoDto>> GetMovieWithUserInfoAsync(Guid userId, Guid movieId)
    {
        var getResult  = await movieRepository.GetByIdAsync(movieId);

        if (!getResult.IsSuccess)
        {
            return Result<MovieWithUserInfoDto>.Failure(getResult.ErrorMessage!)!;
        }
        
        var movie = getResult.Data;
        
        var isInWatchListResult = await userRepository.IsMovieInWatchListAsync(userId, movieId);

        if (!isInWatchListResult.IsSuccess)
        {
            return Result<MovieWithUserInfoDto>.Failure(isInWatchListResult.ErrorMessage!)!;
        }

        var actorWithInfo = new MovieWithUserInfoDto()
        {
            Movie = movie,
            IsInWatchList = isInWatchListResult.Data,
        };
        
        return Result<MovieWithUserInfoDto>.Success(actorWithInfo);
    }
}