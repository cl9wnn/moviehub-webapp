using API.Attributes;
using API.Models.Responses;
using AutoMapper;
using Domain.Abstractions.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Admin;

[Route("api/[controller]")]
[ApiController]
[EntityExists<IUserService, User>]
[Authorize(Roles = "Admin")]
public class AdminUsersController(
    IUserService userService,
    IMapper mapper) : ControllerBase
{
    [HttpGet("all")]
    public async Task<IActionResult> GetUsersAsync()
    {
        var getResult = await userService.GetAllUsersAsync();

        var users = mapper.Map<List<UserResponse>>(getResult.Data);

        return Ok(users);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUserAsync(Guid id)
    {
        var deleteResult = await userService.DeleteUserAsync(id);

        return deleteResult.IsSuccess
            ? Ok()
            : BadRequest(deleteResult.ErrorMessage);
    }
}