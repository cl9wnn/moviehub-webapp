using Domain.Models;

namespace Application.Abstractions;

public interface IAuthService
{
    string GenerateJwtToken(User user);
}