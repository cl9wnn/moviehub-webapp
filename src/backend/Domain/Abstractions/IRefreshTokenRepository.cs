using Application.Utils;
using Domain.Models;

namespace Domain.Abstractions;

public interface IRefreshTokenRepository
{
    Task<Result> AddOrUpdateAsync(RefreshToken refreshToken);
    Task<Result<RefreshToken>> GetByUserIdAsync(Guid userId);
    Task<Result> DeleteAsync(Guid userId);
}
