using System.Security.Claims;
using API.Attributes;
using API.Models.Requests;
using API.Models.Responses;
using AutoMapper;
using Domain.Abstractions.Services;
using Domain.Dtos;
using Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[EntityExists<IMovieService, Movie>]
[Authorize]
[ApiController]
public class MoviesController(IMovieService movieService, IMapper mapper): ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetMovieWithUserInfoAsync(Guid id)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdString, out var userId))
        {
            return Unauthorized("Incorrect format for user");
        }
        
        var getResult = await movieService.GetMovieWithUserInfoAsync(userId, id);
        
        var movie = mapper.Map<MovieWithUserInfoResponse>(getResult.Data);
        
        return getResult.IsSuccess
            ? Ok(movie)
            : NotFound(getResult.ErrorMessage);
    }
}