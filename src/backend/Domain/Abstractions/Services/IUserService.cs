using Domain.Models;
using Domain.Utils;

namespace Domain.Abstractions.Services;

public interface IUserService
{
    Task<Result> RegisterAsync(User user);
}