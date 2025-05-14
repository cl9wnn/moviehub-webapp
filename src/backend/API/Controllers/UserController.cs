using API.Models;
using Domain.Abstractions;
using Domain.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService userService): ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserRequest? request)
    {
        if (request == null)
        {
            return BadRequest(new { Error = "Invalid request body" });
        }
        
        var registerResult = await userService.RegisterAsync(request.Username, request.Email, request.Password);
        
        return registerResult.IsSuccess
            ? Ok()
            : BadRequest(new {Error = registerResult.ErrorMessage});
    }
}