using Application.Utils;

namespace Domain.Abstractions.Services;

public interface IUserService
{
    Task<Result> RegisterAsync(string username, string email, string password);
}