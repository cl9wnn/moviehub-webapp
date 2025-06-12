using Domain.Utils;

namespace Domain.Abstractions.Repositories;

public interface IRepository<TKey, T>
{
    Task<Result<T>> GetByIdAsync(TKey id);
    Task<Result<T>> AddAsync(T dto);
    Task<Result<T>> UpdateAsync(T dto);
    Task<Result> DeleteAsync(TKey id);
    Task<Result<ICollection<T>>> GetAllAsync();
    Task<Result> ExistsAsync(TKey id);
}