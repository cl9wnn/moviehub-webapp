using Application.Utils;
using Domain.Models;

namespace Domain.Abstractions;

public interface IUserService
{
    Task<Result> RegisterAsync(string username, string email, string password);
}