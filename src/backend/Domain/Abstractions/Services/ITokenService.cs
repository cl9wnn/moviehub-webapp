using System.Security.Claims;
using Application.Utils;
using Domain.Models;

namespace Domain.Abstractions.Services;

public interface ITokenService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
    Result<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);
}