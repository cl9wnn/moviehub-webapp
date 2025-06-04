using System.Security.Claims;
using API.Attributes;
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

namespace API.Controllers.Admin;

[Route("api/[controller]")]
[ApiController]
[Authorize]
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
            : NotFound(deleteResult.ErrorMessage);
    }
}