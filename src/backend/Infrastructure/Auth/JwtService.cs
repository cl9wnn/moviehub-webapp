using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Abstractions;
using Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Infrastructure.Auth;

public class JwtService(IOptions<AuthOptions> options): IAuthService
{
    public string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.NameId, user.Id.ToString()),
            new (JwtRegisteredClaimNames.Name, user.Username),
        };

        var singingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SecretKey));
        var singingCredentials = new SigningCredentials(singingKey, SecurityAlgorithms.HmacSha256);

        var jwtToken = new JwtSecurityToken(

            issuer: options.Value.Issuer,
            audience: options.Value.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(options.Value.ExpiredAtMinutes),
            signingCredentials:singingCredentials
        );
        
        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}