using API.Attributes;
using API.Extensions;
using API.Models.Requests;
using API.Models.Responses;
using AutoMapper;
using Domain.Abstractions.Services;
using Domain.Models;
using FluentValidation;
using Infrastructure.Frontend;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace API.Controllers;

[ApiController]
[Authorize]
[EntityExists<ICommentService, Comment>]
[Route("api/[controller]")]
public class CommentsController(ICommentService commentService, IMapper mapper, IOptions<FrontendOptions> frontendOptions): ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCommentAsync(Guid id)
    {
        var getResult = await commentService.GetByIdAsync(id);

        var comment = mapper.Map<CommentResponse>(getResult.Data);
        
        return getResult.IsSuccess
            ? Ok(comment)
            : NotFound(getResult.ErrorMessage);
    }
    
    [HttpPost("{id:guid}/replies")]
    public async Task<IActionResult> CreateReplyCommentAsync(Guid id, [FromBody] CreateCommentRequest request,
        [FromServices] IValidator<CreateCommentRequest> validator)
    {
        if (User.GetUserId() is not Guid userId)
        {
            return Unauthorized("Incorrect format for user id");
        }
        
        var validationResult = await validator.ValidateAsync(request);
        
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .Select(e => new { e.PropertyName, e.ErrorMessage });
            return BadRequest(errors);
        }
        
        var comment = mapper.Map<Comment>(request);
        comment.UserId = userId;
        comment.ParentCommentId = id;

        var frontendUrl = frontendOptions.Value.LocalUrl;
        
        var createResult = await commentService.CreateReplyCommentAsync(userId, comment, frontendUrl);
        
        return createResult.IsSuccess
            ? Created()
            : BadRequest(createResult.ErrorMessage);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteOwnCommentAsync(Guid id)
    {
        if (User.GetUserId() is not Guid userId)
        {
            return Unauthorized("Incorrect format for user id");
        }
        
        var deleteResult = await commentService.DeleteOwnCommentAsync(id, userId);
        
        return deleteResult.IsSuccess
            ? Ok()
            : BadRequest(deleteResult.ErrorMessage);
    }

    [HttpPost("{id:guid}/like")]
    public async Task<IActionResult> LikeCommentAsync(Guid id)
    {
        if (User.GetUserId() is not Guid userId)
        {
            return Unauthorized("Incorrect format for user id");
        }
        
        var addResult = await commentService.LikeCommentAsync(userId, id);

        return addResult.IsSuccess
            ? Ok()
            : BadRequest(new { Error = addResult.ErrorMessage });
    }
    
    [HttpDelete("{id:guid}/like")]
    public async Task<IActionResult> UnlikeCommentAsync(Guid id)
    {
        if (User.GetUserId() is not Guid userId)
        {
            return Unauthorized("Incorrect format for user id");
        }
        
        var deleteResult = await commentService.UnlikeCommentAsync(userId, id);

        return deleteResult.IsSuccess
            ? Ok()
            : BadRequest(new { Error = deleteResult.ErrorMessage });
    }
}