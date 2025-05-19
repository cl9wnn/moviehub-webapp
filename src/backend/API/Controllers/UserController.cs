using API.Models;
using AutoMapper;
using Domain.Abstractions.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService userService, IMapper mapper): ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserRequest? request)
    {
        if (request == null)
        {
            return BadRequest(new { Error = "Invalid request body" });
        }
        
        var userDto = mapper.Map<RegisterUserRequest, User>(request);
        var registerResult = await userService.RegisterAsync(userDto);
        
        return registerResult.IsSuccess
            ? Created()
            : BadRequest(new {Error = registerResult.ErrorMessage});
    }
}