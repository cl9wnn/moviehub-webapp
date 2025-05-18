using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Abstractions;
using Application.Utils;
using Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace API.Pipeline.Auth;

public class TokenService(IOptions<AuthOptions> authOptions): ITokenService
{
    public string GenerateAccessToken(User user)
    {
        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, user.Id.ToString()),
            new (ClaimTypes.Name, user.Username),
        };

        var singingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.Value.SecretKey));
        var singingCredentials = new SigningCredentials(singingKey, SecurityAlgorithms.HmacSha256);

        var jwtToken = new JwtSecurityToken(

            issuer: authOptions.Value.Issuer,
            audience: authOptions.Value.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(authOptions.Value.AccessTokenExpiredAtMinutes),
            signingCredentials:singingCredentials
        );
        
        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public Result<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = authOptions!.Value.Issuer,
            ValidateAudience = true,
            ValidAudience = authOptions.Value.Audience,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions!.Value.SecretKey))
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        
        if (securityToken is JwtSecurityToken jwt &&
            jwt.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            return Result<ClaimsPrincipal>.Success(principal);
        }
        
        return Result<ClaimsPrincipal>.Failure("Invalid token!")!;
    }
}