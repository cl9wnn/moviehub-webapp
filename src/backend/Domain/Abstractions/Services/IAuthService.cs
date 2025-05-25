using Domain.Dtos;
using Domain.Models;
using Domain.Utils;

namespace Domain.Abstractions.Services;

public interface IAuthService
{
    Task<Result<AuthDto>> LoginAsync(User user);
    Task<Result<AuthDto>> RefreshTokenAsync(string accessToken, string refreshToken);
    Task<Result> RevokeRefreshTokenAsync(string username);
}