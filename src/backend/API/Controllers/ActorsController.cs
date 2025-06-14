using API.Attributes;
using API.Extensions;
using API.Models.Responses;
using AutoMapper;
using Domain.Abstractions.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[EntityExists<IActorService, Actor>]
[Authorize]
[ApiController]
public class ActorsController(IActorService actorService, IMapper mapper): ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetActorWithUserInfoAsync(Guid id)
    {
        if (User.GetUserId() is not Guid userId)
        {
            return Unauthorized("Incorrect format for user id");
        }

        var getResult = await actorService.GetActorWithUserInfoAsync(userId, id);
        
        var actor = mapper.Map<ActorWithUserInfoResponse>(getResult.Data);
        
        return getResult.IsSuccess
            ? Ok(actor)
            : NotFound(getResult.ErrorMessage);
    }
}