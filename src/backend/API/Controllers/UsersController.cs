using System.Security.Claims;
using API.Attributes;
using API.Models;
using API.Models.Requests;
using API.Models.Responses;
using API.Pipeline.Auth;
using AutoMapper;
using Domain.Abstractions.Services;
using Domain.Dtos;
using Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsersController(
    IUserService userService,
    IMapper mapper,
    IMediaService mediaService,
    IOptions<AuthOptions> authOptions) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserAsync(Guid id)
    {
        var currentUserIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(currentUserIdString, out var currentUserId))
        {
            return Unauthorized("Incorrect format for user");
        }

        var getResult = await userService.GetUserAsync(id);

        var user = mapper.Map<UserResponse>(getResult.Data);

        user.IsCurrentUser = id == currentUserId;

        return getResult.IsSuccess
            ? Ok(user)
            : NotFound(getResult.ErrorMessage);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserRequest request,
        [FromServices] IValidator<RegisterUserRequest> requestValidator)
    {
        var validationResult = await requestValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .Select(e => new { e.PropertyName, e.ErrorMessage });
            return BadRequest(errors);
        }

        var userDto = mapper.Map<RegisterUserRequest, User>(request);
        var registerResult = await userService.RegisterAsync(userDto);

        if (!registerResult.IsSuccess)
        {
            return BadRequest(new { Error = registerResult.ErrorMessage });
        }

        Response.Cookies.Append("refreshToken", registerResult.Data.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTimeOffset.UtcNow.AddDays(authOptions.Value.RefreshTokenExpiredAtDays)
        });

        return Ok(new { Token = registerResult.Data.AccessToken });
    }

    [ValidateImageFile]
    [HttpPost("avatar")]
    public async Task<IActionResult> UploadAvatarAsync(IFormFile file)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdString, out var userId))
        {
            return Unauthorized("Incorrect format for user");
        }

        var objectName = $"avatars/{userId}{Path.GetExtension(file.FileName)}";
        const string bucketName = "users";
        await using var stream = file.OpenReadStream();

        var urlResult = await mediaService.UploadMediaFile(stream, objectName, file.ContentType, bucketName);

        if (!urlResult.IsSuccess)
        {
            return BadRequest(urlResult.ErrorMessage);
        }

        var avatarUrl = urlResult.Data;

        var addOrUpdateResult = await userService.AddOrUpdateAvatarAsync(avatarUrl, userId);

        return addOrUpdateResult.IsSuccess
            ? Ok()
            : BadRequest(addOrUpdateResult.ErrorMessage);
    }


    [HttpPost("favorite-actors/{actorId:guid}")]
    public async Task<IActionResult> AddFavoriteActorAsync(Guid actorId)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdString, out var userId))
        {
            return Unauthorized("Incorrect format for user");
        }

        var addResult = await userService.AddFavoriteActorAsync(userId, actorId);

        return addResult.IsSuccess
            ? Ok()
            : BadRequest(new { Error = addResult.ErrorMessage });
    }

    [HttpDelete("favorite-actors/{actorId:guid}")]
    public async Task<IActionResult> DeleteFavoriteActorAsync(Guid actorId)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdString, out var userId))
        {
            return Unauthorized("Incorrect format for user");
        }

        var deleteResult = await userService.DeleteFavoriteActorAsync(userId, actorId);

        return deleteResult.IsSuccess
            ? Ok()
            : BadRequest(new { Error = deleteResult.ErrorMessage });
    }

    [HttpPost("watchlist/{movieId:guid}")]
    public async Task<IActionResult> AddToWatchListAsync(Guid movieId)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdString, out var userId))
        {
            return Unauthorized("Incorrect format for user");
        }

        var addResult = await userService.AddToWatchListAsync(userId, movieId);

        return addResult.IsSuccess
            ? Ok()
            : BadRequest(new { Error = addResult.ErrorMessage });
    }

    [HttpDelete("watchlist/{movieId:guid}")]
    public async Task<IActionResult> DeleteFromWatchListAsync(Guid movieId)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdString, out var userId))
        {
            return Unauthorized("Incorrect format for user");
        }

        var deleteResult = await userService.DeleteFromWatchListAsync(userId, movieId);

        return deleteResult.IsSuccess
            ? Ok()
            : BadRequest(new { Error = deleteResult.ErrorMessage });
    }

    [HttpPatch("personalize")]
    public async Task<IActionResult> PersonalizeUserAsync([FromBody] PersonalizeUserRequest request,
        [FromServices] IValidator<PersonalizeUserRequest> requestValidator)
    {
        var validationResult = await requestValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .Select(e => new { e.PropertyName, e.ErrorMessage });
            return BadRequest(errors);
        }

        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdString, out var userId))
        {
            return Unauthorized("Incorrect format for user");
        }

        var personalizeDto = mapper.Map<PersonalizeUserRequest, PersonalizeUserDto>(request);

        var updateResult = await userService.PersonalizeUserAsync(personalizeDto, userId);

        return updateResult.IsSuccess
            ? Ok()
            : BadRequest(new { Error = updateResult.ErrorMessage });
    }
}