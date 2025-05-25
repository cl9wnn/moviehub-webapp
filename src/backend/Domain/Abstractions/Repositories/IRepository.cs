using Domain.Utils;

namespace Domain.Abstractions.Repositories;

public interface IRepository<TKey, T>
{
    Task<Result<T>> GetByIdAsync(TKey id);
    Task<Result<T>> AddAsync(T entity);
    Task<Result<T>> UpdateAsync(T entity);
    Task<Result> DeleteAsync(TKey id);
    Task<Result<ICollection<T>>> GetAllAsync();
}