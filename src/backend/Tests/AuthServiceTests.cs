using Application.Services;
using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
using Domain.Dtos;
using Domain.Models;
using Domain.Utils;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Security.Claims;
using Xunit;

public class AuthServiceTests
{
    private readonly Mock<IRefreshTokenRepository> _refreshTokenRepoMock = new();
    private readonly Mock<ITokenService> _tokenServiceMock = new();
    private readonly Mock<IUserRepository> _userRepoMock = new();
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        _authService = new AuthService(
            _refreshTokenRepoMock.Object,
            _tokenServiceMock.Object,
            _userRepoMock.Object
        );
    }

    [Fact]
    public async Task LoginAsync_ReturnsAuthDto_WhenCredentialsAreValid()
    {
        var user = new User { Id = Guid.NewGuid(), Username = "test", Password = new PasswordHasher<User>().HashPassword(null!, "password") };

        _userRepoMock.Setup(r => r.GetByUsernameAsync(user.Username))
            .ReturnsAsync(Result<User>.Success(user));

        _tokenServiceMock.Setup(t => t.GenerateAccessToken(user)).Returns("access-token");
        _tokenServiceMock.Setup(t => t.GenerateRefreshToken()).Returns("refresh-token");

        _refreshTokenRepoMock.Setup(r => r.AddOrUpdateAsync(It.IsAny<RefreshToken>()))
            .ReturnsAsync(Result.Success());

        var loginInput = new User { Username = user.Username, Password = "password" };

        var result = await _authService.LoginAsync(loginInput);

        Assert.True(result.IsSuccess);
        Assert.Equal("access-token", result.Data.AccessToken);
        Assert.Equal("refresh-token", result.Data.RefreshToken);
    }

    [Fact]
    public async Task LoginAsync_ReturnsFailure_WhenUserNotFound()
    {
        _userRepoMock.Setup(r => r.GetByUsernameAsync("test"))
            .ReturnsAsync(Result<User>.Failure("User not found"));

        var result = await _authService.LoginAsync(new User { Username = "test", Password = "123" });

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task LoginAsync_ReturnsFailure_WhenPasswordIsInvalid()
    {
        var user = new User { Username = "test", Password = new PasswordHasher<User>().HashPassword(null!, "correct") };
        _userRepoMock.Setup(r => r.GetByUsernameAsync("test"))
            .ReturnsAsync(Result<User>.Success(user));

        var result = await _authService.LoginAsync(new User { Username = "test", Password = "wrong" });

        Assert.False(result.IsSuccess);
        Assert.Equal("Invalid password", result.ErrorMessage);
    }

    [Fact]
    public async Task RefreshTokenAsync_ReturnsNewTokens_WhenValid()
    {
        var user = new User { Id = Guid.NewGuid(), Username = "user" };
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.Username) }));

        _tokenServiceMock.Setup(t => t.GetPrincipalFromExpiredToken("access"))
            .Returns(Result<ClaimsPrincipal>.Success(claimsPrincipal));

        _userRepoMock.Setup(r => r.GetByUsernameAsync(user.Username))
            .ReturnsAsync(Result<User>.Success(user));

        var existingRefresh = RefreshToken.Create("existing-token", user.Id);
        _refreshTokenRepoMock.Setup(r => r.GetByUserIdAsync(user.Id))
            .ReturnsAsync(Result<RefreshToken>.Success(existingRefresh));

        _tokenServiceMock.Setup(t => t.GenerateAccessToken(user)).Returns("new-access");
        _tokenServiceMock.Setup(t => t.GenerateRefreshToken()).Returns("new-refresh");

        _refreshTokenRepoMock.Setup(r => r.AddOrUpdateAsync(It.IsAny<RefreshToken>()))
            .ReturnsAsync(Result.Success());

        var result = await _authService.RefreshTokenAsync("access", "existing-token");

        Assert.True(result.IsSuccess);
        Assert.Equal("new-access", result.Data.AccessToken);
        Assert.Equal("new-refresh", result.Data.RefreshToken);
    }

    [Fact]
    public async Task RefreshTokenAsync_ReturnsFailure_WhenPrincipalInvalid()
    {
        _tokenServiceMock.Setup(t => t.GetPrincipalFromExpiredToken("bad"))
            .Returns(Result<ClaimsPrincipal>.Failure("Invalid"));

        var result = await _authService.RefreshTokenAsync("bad", "refresh");

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task RefreshTokenAsync_ReturnsFailure_WhenUserNotFound()
    {
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "user") }));
        _tokenServiceMock.Setup(t => t.GetPrincipalFromExpiredToken("access"))
            .Returns(Result<ClaimsPrincipal>.Success(claimsPrincipal));

        _userRepoMock.Setup(r => r.GetByUsernameAsync("user"))
            .ReturnsAsync(Result<User>.Failure("User not found"));

        var result = await _authService.RefreshTokenAsync("access", "token");

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task RefreshTokenAsync_ReturnsFailure_WhenTokenDoesNotMatch()
    {
        var user = new User { Id = Guid.NewGuid(), Username = "user" };
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.Username) }));

        _tokenServiceMock.Setup(t => t.GetPrincipalFromExpiredToken("access"))
            .Returns(Result<ClaimsPrincipal>.Success(claimsPrincipal));

        _userRepoMock.Setup(r => r.GetByUsernameAsync("user"))
            .ReturnsAsync(Result<User>.Success(user));

        var dbToken = RefreshToken.Create("not-matching", user.Id);
        _refreshTokenRepoMock.Setup(r => r.GetByUserIdAsync(user.Id))
            .ReturnsAsync(Result<RefreshToken>.Success(dbToken));

        var result = await _authService.RefreshTokenAsync("access", "wrong-token");

        Assert.False(result.IsSuccess);
        Assert.Equal("Invalid refresh token", result.ErrorMessage);
    }

    [Fact]
    public async Task RefreshTokenAsync_ReturnsFailure_WhenTokenExpired()
    {
        var user = new User { Id = Guid.NewGuid(), Username = "user" };
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.Username) }));

        _tokenServiceMock.Setup(t => t.GetPrincipalFromExpiredToken("access"))
            .Returns(Result<ClaimsPrincipal>.Success(claimsPrincipal));

        _userRepoMock.Setup(r => r.GetByUsernameAsync("user"))
            .ReturnsAsync(Result<User>.Success(user));

        var expiredToken = RefreshToken.Create("token", user.Id);
        expiredToken.Expires = DateTime.UtcNow.AddMinutes(-1);

        _refreshTokenRepoMock.Setup(r => r.GetByUserIdAsync(user.Id))
            .ReturnsAsync(Result<RefreshToken>.Success(expiredToken));

        var result = await _authService.RefreshTokenAsync("access", "token");

        Assert.False(result.IsSuccess);
        Assert.Equal("Refresh token has expired", result.ErrorMessage);
    }

    [Fact]
    public async Task RevokeRefreshTokenAsync_ReturnsSuccess_WhenUserExists()
    {
        var user = new User { Id = Guid.NewGuid(), Username = "user" };

        _userRepoMock.Setup(r => r.GetByUsernameAsync("user"))
            .ReturnsAsync(Result<User>.Success(user));

        _refreshTokenRepoMock.Setup(r => r.DeleteAsync(user.Id))
            .ReturnsAsync(Result.Success());

        var result = await _authService.RevokeRefreshTokenAsync("user");

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task RevokeRefreshTokenAsync_ReturnsFailure_WhenUserNotFound()
    {
        _userRepoMock.Setup(r => r.GetByUsernameAsync("user"))
            .ReturnsAsync(Result<User>.Failure("Not found"));

        var result = await _authService.RevokeRefreshTokenAsync("user");

        Assert.False(result.IsSuccess);
        Assert.Equal("Not found", result.ErrorMessage);
    }
}
