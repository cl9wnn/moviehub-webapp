using System.Security.Claims;
using Application.Utils;
using Domain.Models;

namespace Application.Abstractions;

public interface ITokenService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
    Result<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);
}