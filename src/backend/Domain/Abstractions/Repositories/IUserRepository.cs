using Application.Utils;
using Domain.Models;

namespace Domain.Abstractions.Repositories;

public interface IUserRepository
{
    Task<Result<User>> GetByIdAsync(Guid userId);
    Task<Result<User>> GetByUsernameAsync(string username);
    Task<Result> AddAsync(User user);
}