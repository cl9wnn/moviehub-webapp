using System.Security.Claims;
using API.Attributes;
using API.Models.Requests;
using API.Models.Responses;
using AutoMapper;
using Domain.Abstractions.Services;
using Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ActorExists]
[Authorize]
[ApiController]
public class ActorsController(IActorService actorService, IMapper mapper): ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetActorWithUserInfoAsync(Guid id)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdString, out var userId))
        {
            return Unauthorized("Incorrect format for user");
        }

        var getResult = await actorService.GetActorWithUserInfoAsync(userId, id);
        
        var actor = mapper.Map<ActorWithUserInfoResponse>(getResult.Data);
        
        return getResult.IsSuccess
            ? Ok(actor)
            : NotFound(getResult.ErrorMessage);
    }
}