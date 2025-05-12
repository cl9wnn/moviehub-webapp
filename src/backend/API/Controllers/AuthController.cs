using API.Models;
using Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IUserService userService):ControllerBase
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
    
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginUserRequest? request)
    {
        if (request == null)
        {
            return BadRequest(new { Error = "Invalid request body" });
        }
        
        var loginResult = await userService.LoginAsync(request.Username, request.Password);
        
        return loginResult.IsSuccess
            ? Ok(new {Token = loginResult.Data})
            : BadRequest(new {Error = loginResult.ErrorMessage});
    }
}