using API.Models;
using API.Pipeline.Auth;
using AutoMapper;
using Domain.Abstractions.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService, IOptions<AuthOptions> authOptions, IMapper mapper):ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginUserRequest? request)
    {
        if (request == null)
        {
            return BadRequest(new { Error = "Invalid request body" });
        }

        var userDto = mapper.Map<LoginUserRequest, User>(request);
        var loginResult = await authService.LoginAsync(userDto);

        if (!loginResult.IsSuccess)
        {
            return BadRequest(new { Error = loginResult.ErrorMessage });
        }
        
        Response.Cookies.Append("refreshToken", loginResult.Data.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTimeOffset.UtcNow.AddDays(authOptions.Value.RefreshTokenExpiredAtDays)
        });
        
        return Ok(new { Token = loginResult.Data.AccessToken });
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshTokenAsync([FromBody] AccessTokenRequest accessTokenRequest)
    {
        var refreshToken = Request.Cookies["refreshToken"];
        var accessToken = accessTokenRequest.AccessToken;
        
        if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
            return BadRequest("Missing tokens");
        
        var refreshResult = await authService.RefreshTokenAsync(accessToken, refreshToken);

        if (!refreshResult.IsSuccess)
        {
            return BadRequest(new { Error = refreshResult.ErrorMessage });
        }
        
        Response.Cookies.Append("refreshToken", refreshResult.Data.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTimeOffset.UtcNow.AddDays(authOptions.Value.RefreshTokenExpiredAtDays)
        });
        
        return Ok( new {Token = refreshResult.Data.AccessToken});
    }

    [Authorize]
    [HttpPost("revoke")]
    public async Task<IActionResult> RevokeRefreshTokenAsync()
    {
        var username = User.Identity.Name;

        if (string.IsNullOrEmpty(username))
        {
            return Unauthorized();
        }
        
        var revokeResult = await authService.RevokeRefreshTokenAsync(username);

        if (!revokeResult.IsSuccess)
        {
            return BadRequest(new { Error = revokeResult.ErrorMessage });
        }
        
        Response.Cookies.Delete("refreshToken");
        return NoContent();
    }
}