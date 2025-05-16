using Application.Abstractions;
using Application.Utils;
using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class AuthService(IRefreshTokenRepository refreshTokenRepository, ITokenService tokenService, 
    IUserRepository userRepository): IAuthService
{
      public async Task<Result<AuthModel>> LoginAsync(string username, string password)
    {
        var userResult = await userRepository.GetByUsernameAsync(username);

        if (!userResult.IsSuccess)
        {
            return Result<AuthModel>.Failure(userResult.ErrorMessage!)!;
        }

        var user = userResult.Data;
        var verifyResult = new PasswordHasher<User>().VerifyHashedPassword(user, user.Password, password);

        if (verifyResult != PasswordVerificationResult.Success)
        {
            return Result<AuthModel>.Failure("Invalid password")!;
        }
        
        var accessToken = tokenService.GenerateAccessToken(user);
        var refreshToken = RefreshToken.Create(tokenService.GenerateRefreshToken(), user.Id);
        
        var updateResult = await refreshTokenRepository.AddOrUpdateAsync(refreshToken);

        if (!updateResult.IsSuccess)
        {
            return Result<AuthModel>.Failure("Invalid refresh token")!;
        }
        
        return Result<AuthModel>.Success(AuthModel.Create(accessToken, refreshToken.Token));
    }

    public async Task<Result<AuthModel>> RefreshTokenAsync(string accessToken, string refreshToken)
    {
        var principalResult = tokenService.GetPrincipalFromExpiredToken(accessToken);
        
        if (!principalResult.IsSuccess)
        {
            return Result<AuthModel>.Failure(principalResult.ErrorMessage!)!;
        }

        var username = principalResult.Data.Identity!.Name!;
        var userResult = await userRepository.GetByUsernameAsync(username);

        if (!userResult.IsSuccess)
        {
            return Result<AuthModel>.Failure(userResult.ErrorMessage!)!;
        }

        var refreshTokenResult = await refreshTokenRepository.GetByUserIdAsync(userResult.Data.Id);

        if (!refreshTokenResult.IsSuccess || refreshTokenResult.Data.Token != refreshToken)
        {
            return Result<AuthModel>.Failure("Invalid refresh token")!;
        }
        
        if (refreshTokenResult.Data.Expires <= DateTime.UtcNow)
        {
            return Result<AuthModel>.Failure("Refresh token has expired")!;
        }
        
        var newAccessToken = tokenService.GenerateAccessToken(userResult.Data);
        var newRefreshToken = RefreshToken.Create(tokenService.GenerateRefreshToken(), userResult.Data.Id);
        
        var updateResult = await refreshTokenRepository.AddOrUpdateAsync(newRefreshToken);

        if (!updateResult.IsSuccess)
        {
            return Result<AuthModel>.Failure("Invalid refresh token!")!;
        }
        
        return Result<AuthModel>.Success(AuthModel.Create(newAccessToken, newRefreshToken.Token));
    }

    public async Task<Result> RevokeRefreshTokenAsync(string username)
    {
        var userResult = await userRepository.GetByUsernameAsync(username);

        if (!userResult.IsSuccess)
        {
            return Result.Failure(userResult.ErrorMessage!)!;
        }
        
        var deleteResult = await refreshTokenRepository.DeleteAsync(userResult.Data.Id);
        
        return deleteResult.IsSuccess
            ? Result.Success()
            : Result.Failure(deleteResult.ErrorMessage!)!;
    }
}