using Domain.Models;
using Domain.Utils;

namespace Domain.Abstractions.Repositories;

public interface IRefreshTokenRepository
{
    Task<Result> AddOrUpdateAsync(RefreshToken refreshToken);
    Task<Result<RefreshToken>> GetByUserIdAsync(Guid userId);
    Task<Result> DeleteAsync(Guid userId);
}
