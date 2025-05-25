using System.Security.Claims;
using Domain.Models;
using Domain.Utils;

namespace Domain.Abstractions.Services;

public interface ITokenService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
    Result<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);
}