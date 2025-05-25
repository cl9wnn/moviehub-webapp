using Domain.Dtos;
using Domain.Models;
using Domain.Utils;

namespace Domain.Abstractions.Repositories;

public interface IMovieRepository: IRepository<Guid, Movie>
{
    Task<Result> AddActorsAsync(List<MovieActorDto> actors);
    Task<Result> AddOrUpdatePosterPhotoAsync(string url, Guid id);
    Task<Result> AddMoviePhotoAsync(Photo photo, Guid id);
}