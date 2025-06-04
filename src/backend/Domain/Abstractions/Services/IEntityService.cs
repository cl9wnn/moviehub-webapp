using Domain.Utils;

namespace Domain.Abstractions.Services;

public interface IEntityService<TDto>
{
    Task<Result<TDto>> GetByIdAsync(Guid id);
}