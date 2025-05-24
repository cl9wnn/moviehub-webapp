using API.Models;
using API.Models.Requests;
using AutoMapper;
using Domain.Abstractions.Services;
using Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService userService, IMapper mapper): ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserRequest request, 
        [FromServices] IValidator<RegisterUserRequest> requestValidator)
    {
        var validationResult = await requestValidator.ValidateAsync(request);
        
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .Select(e => new { e.PropertyName, e.ErrorMessage });
            return BadRequest(errors);
        }
        
        var userDto = mapper.Map<RegisterUserRequest, User>(request);
        var registerResult = await userService.RegisterAsync(userDto);
        
        return registerResult.IsSuccess
            ? Created()
            : BadRequest(new {Error = registerResult.ErrorMessage});
    }
}