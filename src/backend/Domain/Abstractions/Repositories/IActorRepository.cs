using Domain.Models;

namespace Domain.Abstractions.Repositories;

public interface IActorRepository: IRepository<Guid, Actor>
{
    
}