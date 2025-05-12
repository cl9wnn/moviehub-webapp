using Application.Utils;

namespace Domain.Abstractions;

public interface IUserService
{
    Task<Result> RegisterAsync(string username, string email, string password);
    Task<Result<string>> LoginAsync(string username, string password);
}