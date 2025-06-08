using API.Attributes;
using API.Extensions;
using API.Models.Requests;
using API.Models.Responses;
using AutoMapper;
using Domain.Abstractions.Services;
using Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Authorize]
[EntityExists<IDiscussionTopicService, DiscussionTopic>]
[Route("api/[controller]")]
public class DiscussionTopicsController(IDiscussionTopicService topicService, IMapper mapper): ControllerBase
{
    [HttpGet("all")]
    public async Task<IActionResult> GetDiscussionTopicsAsync()
    {
        var getResult = await topicService.GetAllTopicsAsync();
        
        var topics = mapper.Map<List<DiscussionTopicResponse>>(getResult.Data);
        
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

    [HttpPost]
    public async Task<IActionResult> CreateDiscussionTopicAsync([FromBody] CreateDiscussionTopicRequest request,
        [FromServices] IValidator<CreateDiscussionTopicRequest> validator)
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
        
        var topic = mapper.Map<DiscussionTopic>(request);
        var createResult = await topicService.CreateTopicAsync(topic, userId);
        
        return createResult.IsSuccess
            ? Created()
            : BadRequest(createResult.ErrorMessage);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteDiscussionTopicAsync(Guid id)
    {
        var deleteResult = await topicService.DeleteTopicAsync(id);
        
        return deleteResult.IsSuccess
            ? Ok()
            : NotFound(deleteResult.ErrorMessage);
    }
}