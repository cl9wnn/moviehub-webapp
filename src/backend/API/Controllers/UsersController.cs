using API.Attributes;
using API.Extensions;
using API.Models.Requests;
using API.Models.Responses;
using API.Pipeline.Auth;
using AutoMapper;
using Domain.Abstractions.Services;
using Domain.Dtos;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[EntityExists<IUserService, User>]
[Authorize]
public class UsersController(
    IUserService userService,
    IMapper mapper,
    IMediaService mediaService,
    IOptions<AuthOptions> authOptions,
    IOptions<AdminOptions> adminOptions) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserAsync(Guid id)
    {
        if (User.GetUserId() is not Guid userId)
        {
            return Unauthorized("Incorrect format for user id");
        }

        var getResult = await userService.GetByIdAsync(id);

        var user = mapper.Map<UserResponse>(getResult.Data);
        
        user.IsCurrentUser = id == userId;
        
        return getResult.IsSuccess
            ? Ok(user)
            : NotFound(getResult.ErrorMessage);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserRequest request)
    {
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

    [AllowAnonymous]
    [HttpPost("register-admin")]
    public async Task<IActionResult> RegisterAdminAsync([FromBody] RegisterAdminRequest request)
    {
        if (request.SecretKey != adminOptions.Value.SecretKey)
        {
            return Unauthorized("Invalid secret key!");
        }
        
        var userDto = mapper.Map<RegisterAdminRequest, User>(request);
        userDto.Role = UserRole.Admin;
        
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
        if (User.GetUserId() is not Guid userId)
        {
            return Unauthorized("Incorrect format for user id");
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

    [HttpGet("{id:guid}/comments")]
    public async Task<IActionResult> GetCommentsByUserIdAsync(Guid id)
    {
        var getResult = await userService.GetCommentsByUserIdAsync(id);

        var comment = mapper.Map<List<UserCommentResponse>>(getResult.Data);
        
        return getResult.IsSuccess
            ? Ok(comment)
            : NotFound(getResult.ErrorMessage);
    }
    
    [HttpGet("{id:guid}/topics")]
    public async Task<IActionResult> GetTopicsByUserIdAsync(Guid id)
    {
        var getResult = await userService.GetTopicsByUserIdAsync(id);

        var topic = mapper.Map<List<UserDiscussionTopicResponse>>(getResult.Data);
        
        return getResult.IsSuccess
            ? Ok(topic)
            : NotFound(getResult.ErrorMessage);
    }

    [HttpPost("favorite-actors/{actorId:guid}")]
    public async Task<IActionResult> AddFavoriteActorAsync(Guid actorId)
    {
        if (User.GetUserId() is not Guid userId)
        {
            return Unauthorized("Incorrect format for user id");
        }

        var addResult = await userService.AddFavoriteActorAsync(userId, actorId);

        return addResult.IsSuccess
            ? Ok()
            : BadRequest(new { Error = addResult.ErrorMessage });
    }

    [HttpDelete("favorite-actors/{actorId:guid}")]
    public async Task<IActionResult> DeleteFavoriteActorAsync(Guid actorId)
    {
        if (User.GetUserId() is not Guid userId)
        {
            return Unauthorized("Incorrect format for user id");
        }

        var deleteResult = await userService.DeleteFavoriteActorAsync(userId, actorId);

        return deleteResult.IsSuccess
            ? Ok()
            : BadRequest(new { Error = deleteResult.ErrorMessage });
    }

    [HttpPost("watchlist/{movieId:guid}")]
    public async Task<IActionResult> AddToWatchListAsync(Guid movieId)
    {
        if (User.GetUserId() is not Guid userId)
        {
            return Unauthorized("Incorrect format for user id");
        }

        var addResult = await userService.AddToWatchListAsync(userId, movieId);

        return addResult.IsSuccess
            ? Ok()
            : BadRequest(new { Error = addResult.ErrorMessage });
    }

    [HttpDelete("watchlist/{movieId:guid}")]
    public async Task<IActionResult> DeleteFromWatchListAsync(Guid movieId)
    {
        if (User.GetUserId() is not Guid userId)
        {
            return Unauthorized("Incorrect format for user id");
        }

        var deleteResult = await userService.DeleteFromWatchListAsync(userId, movieId);

        return deleteResult.IsSuccess
            ? Ok()
            : BadRequest(new { Error = deleteResult.ErrorMessage });
    }
    
    [HttpPost("not-interested/{movieId:guid}")]
    public async Task<IActionResult> AddToNotInterestedAsync(Guid movieId)
    {
        if (User.GetUserId() is not Guid userId)
        {
            return Unauthorized("Incorrect format for user id");
        }

        var addResult = await userService.AddToNotInterestedAsync(userId, movieId);

        return addResult.IsSuccess
            ? Ok()
            : BadRequest(new { Error = addResult.ErrorMessage });
    }
    
    [HttpDelete("not-interested/{movieId:guid}")]
    public async Task<IActionResult> DeleteFromNotInterestedAsync(Guid movieId)
    {
        if (User.GetUserId() is not Guid userId)
        {
            return Unauthorized("Incorrect format for user id");
        }

        var deleteResult = await userService.DeleteFromNotInterestedAsync(userId, movieId);

        return deleteResult.IsSuccess
            ? Ok()
            : BadRequest(new { Error = deleteResult.ErrorMessage });
    }

    [HttpPatch("personalize")]
    public async Task<IActionResult> PersonalizeUserAsync([FromBody] PersonalizeUserRequest request)
    {
        if (User.GetUserId() is not Guid userId)
        {
            return Unauthorized("Incorrect format for user id");
        }

        var personalizeDto = mapper.Map<PersonalizeUserRequest, PersonalizeUserDto>(request);

        var updateResult = await userService.PersonalizeUserAsync(personalizeDto, userId);

        return updateResult.IsSuccess
            ? Ok()
            : BadRequest(new { Error = updateResult.ErrorMessage });
    }
}