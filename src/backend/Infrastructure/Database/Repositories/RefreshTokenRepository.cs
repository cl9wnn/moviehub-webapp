using Application.Utils;
using AutoMapper;
using Domain.Abstractions;
using Domain.Models;
using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

public class RefreshTokenRepository(AppDbContext dbContext, IMapper mapper): IRefreshTokenRepository
{
    public async Task<Result> AddOrUpdateAsync(RefreshToken refreshToken)
    {
        var existingTokenEntity = await dbContext.RefreshTokens
            .FirstOrDefaultAsync(t => t.UserId == refreshToken.UserId);
        
        if (existingTokenEntity is not null)
        {
            existingTokenEntity.Token = refreshToken.Token;
            existingTokenEntity.Expires = refreshToken.Expires;
            existingTokenEntity.Created = refreshToken.Created;
        }
        else
        {
            var newTokenEntity = mapper.Map<RefreshTokenEntity>(refreshToken);
            await dbContext.RefreshTokens.AddAsync(newTokenEntity);
        }

        await dbContext.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<Result<RefreshToken>> GetByUserIdAsync(Guid userId)
    {
        var refreshTokenEntity = await dbContext.RefreshTokens
            .FirstOrDefaultAsync(t => t.UserId == userId);

        if (refreshTokenEntity is null)
        {
            return Result<RefreshToken>.Failure("Refresh token not found")!;
        }
        
        var refreshToken = mapper.Map<RefreshToken>(refreshTokenEntity);
        
        return Result<RefreshToken>.Success(refreshToken);
    }

    public async Task<Result> DeleteAsync(Guid userId)
    {
        var refreshTokenEntity = await dbContext.RefreshTokens
            .FirstOrDefaultAsync(t => t.UserId == userId);

        if (refreshTokenEntity is null)
        {
            return Result.Failure("Refresh token not found")!;
        }

        dbContext.RefreshTokens.Remove(refreshTokenEntity);
        await dbContext.SaveChangesAsync();
        return Result.Success();
    }
}