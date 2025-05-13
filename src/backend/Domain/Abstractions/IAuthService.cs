using Application.Utils;
using Domain.Models;

namespace Domain.Abstractions;

public interface IAuthService
{
    Task<Result<AuthModel>> LoginAsync(string username, string password);
    Task<Result<AuthModel>> RefreshTokenAsync(string accessToken, string refreshToken);
    Task<Result> RevokeRefreshTokenAsync(string username);
}