using Application.Utils;
using Domain.Models;

namespace Domain.Abstractions.Services;

public interface IUserService
{
    Task<Result> RegisterAsync(User user);
}