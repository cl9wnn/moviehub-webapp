using Application.Utils;
using Domain.Dtos;
using Domain.Models;

namespace Domain.Abstractions.Repositories;

public interface IMovieRepository: IRepository<Guid, Movie>
{
    Task<Result> AddActorsAsync(List<MovieActorDto> actors);
}