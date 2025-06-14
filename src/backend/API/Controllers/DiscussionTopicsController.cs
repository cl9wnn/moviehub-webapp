using API.Attributes;
using API.Extensions;
using API.Models.Requests;
using API.Models.Responses;
using AutoMapper;
using Domain.Abstractions.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Authorize]
[EntityExists<IDiscussionTopicService, DiscussionTopic>]
[Route("api/[controller]")]
public class DiscussionTopicsController(IDiscussionTopicService topicService, ICommentService commentService,
    IMapper mapper): ControllerBase
{
    [HttpGet("all")]
    public async Task<IActionResult> GetDiscussionTopicsAsync()
    {
        var getResult = await topicService.GetAllTopicsAsync();
        
        var topics = mapper.Map<List<ListDiscussionTopicResponse>>(getResult.Data);
        
        return Ok(topics);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetDiscussionTopicByIdAsync(Guid id)
    {
        var getResult = await topicService.GetByIdAsync(id);

        var topic = mapper.Map<DiscussionTopicResponse>(getResult.Data);
        
        return getResult.IsSuccess
            ? Ok(topic)
            : NotFound(getResult.ErrorMessage);
    }
    
    [HttpGet("paginated")]
    public async Task<IActionResult> GetDiscussionTopicsPaginated([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        if (User.GetUserId() is not Guid userId)
        {
            return Unauthorized("Incorrect format for user id");
        }

        var result = await topicService.GetPaginatedTopicsAsync(userId, page, pageSize);

        if (!result.IsSuccess)
            return BadRequest(result.ErrorMessage);

        var response = new PaginatedResponse<ListDiscussionTopicResponse>
        {
            Items = mapper.Map<ICollection<ListDiscussionTopicResponse>>(result.Data.Items),
            TotalCount = result.Data.TotalCount
        };

        return Ok(response);
    }
    
    [HttpGet("{id:guid}/comments")]
    public async Task<IActionResult> GetCommentsByTopicIdAsync(Guid id)
    {
        var getResult = await topicService.GetCommentsByTopicIdAsync(id);

        var topic = mapper.Map<List<CommentResponse>>(getResult.Data);
        
        return getResult.IsSuccess
            ? Ok(topic)
            : NotFound(getResult.ErrorMessage);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDiscussionTopicAsync([FromBody] CreateDiscussionTopicRequest request)
    {
        if (User.GetUserId() is not Guid userId)
        {
            return Unauthorized("Incorrect format for user id");
        }
        
        var topic = mapper.Map<DiscussionTopic>(request);
        topic.UserId = userId;
        
        var createResult = await topicService.CreateTopicAsync(topic);
        
        return createResult.IsSuccess
            ? Ok( new {Id = createResult.Data.Id})
            : BadRequest(createResult.ErrorMessage);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteDiscussionTopicAsync(Guid id)
    {
        var deleteResult = await topicService.DeleteTopicAsync(id);
        
        return deleteResult.IsSuccess
            ? Ok()
            : BadRequest(deleteResult.ErrorMessage);
    }
    
    [HttpPost("{id:guid}/comments")]
    public async Task<IActionResult> CreateCommentToTopicAsync(Guid id, [FromBody] CreateCommentRequest request)
    {
        if (User.GetUserId() is not Guid userId)
        {
            return Unauthorized("Incorrect format for user id");
        }
        
        var comment = mapper.Map<Comment>(request);
        comment.UserId = userId;
        comment.TopicId = id;
        
        var createResult = await commentService.CreateTopicCommentAsync(comment);
        
        return createResult.IsSuccess
            ? Created()
            : BadRequest(createResult.ErrorMessage);
    }
}