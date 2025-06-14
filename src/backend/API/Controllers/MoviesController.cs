using API.Attributes;
using API.Extensions;
using API.Models.Requests;
using API.Models.Responses;
using AutoMapper;
using Domain.Abstractions.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[EntityExists<IMovieService, Movie>]
[Authorize]
[ApiController]
public class MoviesController(IMovieService movieService, IMapper mapper): ControllerBase
{
    [HttpGet("all")]
    public async Task<IActionResult> GetMoviesForSearchAsync()
    {
        var getResult = await movieService.GetAllMoviesAsync();
        
        var movies = mapper.Map<List<MovieSearchResponse>>(getResult.Data);
        
        return Ok(movies);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetMovieWithUserInfoAsync(Guid id)
    {
        if (User.GetUserId() is not Guid userId)
        {
            return Unauthorized("Incorrect format for user id");
        }
        
        var getResult = await movieService.GetMovieWithUserInfoAsync(userId, id);
        
        var movie = mapper.Map<MovieWithUserInfoResponse>(getResult.Data);
        
        return getResult.IsSuccess
            ? Ok(movie)
            : NotFound(getResult.ErrorMessage);
    }

    [HttpPost("{id:guid}/rate")]
    public async Task<IActionResult> RateMovieAsync(Guid id, [FromBody] MovieRatingRequest request)
    {
        if (User.GetUserId() is not Guid userId)
        {
            return Unauthorized("Incorrect format for user id");
        }

        if (request.Rating < 1 || request.Rating > 10)
        {
            return BadRequest("Rating must be between 1 and 10");
        }

        var rateResult = await movieService.RateMovieAsync(id, userId, request.Rating);
        
        return rateResult.IsSuccess
            ? Ok()
            : BadRequest(new {Error = rateResult.ErrorMessage});
    }
    
    [HttpPost("{id:guid}/topics")]
    public async Task<IActionResult> GetTopicsByUserIdAsync(Guid id)
    {
        var getResult = await movieService.GetTopicsByMovieIdAsync(id);

        var topic = mapper.Map<List<MovieDiscussionTopicResponse>>(getResult.Data);
        
        return getResult.IsSuccess
            ? Ok(topic)
            : NotFound(getResult.ErrorMessage);
    }
}