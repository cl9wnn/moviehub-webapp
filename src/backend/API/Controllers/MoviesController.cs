using API.Attributes;
using API.Models.Requests;
using API.Models.Responses;
using AutoMapper;
using Domain.Abstractions.Services;
using Domain.Dtos;
using Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[MovieExists]
[ApiController]
public class MoviesController(IMovieService movieService , IMapper mapper): ControllerBase
{
    [HttpGet("all")]
    public async Task<IActionResult> GetMoviesAsync()
    {
        var getResult = await movieService.GetAllMoviesAsync();
        
        var movies = mapper.Map<List<MovieResponse>>(getResult.Data);
        
        return Ok(movies);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetMovieAsync(Guid id)
    {
        var getResult = await movieService.GetMovieAsync(id);
        
        var movie = mapper.Map<MovieResponse>(getResult.Data);
        
        return getResult.IsSuccess
            ? Ok(movie)
            : NotFound(getResult.ErrorMessage);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMovieAsync([FromBody] CreateMovieRequest createMovieRequest,
        [FromServices] IValidator<CreateMovieRequest> validator)
    {
        var validationResult = await validator.ValidateAsync(createMovieRequest);
        
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .Select(e => new { e.PropertyName, e.ErrorMessage });
            return BadRequest(errors);
        }
        
        var movie = mapper.Map<Movie>(createMovieRequest);
        var createResult = await movieService.CreateMovieAsync(movie);
        
        return createResult.IsSuccess
            ? Created()
            : BadRequest(createResult.ErrorMessage);
    }
    
    [HttpPost("{id:guid}/actors")]
    public async Task<IActionResult> AddActorsAsync(Guid id, [FromBody] List<CreateMovieActorByIdRequest> createActorRequest)
    {
        var movieActors = mapper.Map<List<MovieActorDto>>(createActorRequest, opt => 
            opt.Items["MovieId"] = id);
        
        var addResult = await movieService.AddActorsAsync(movieActors);
        
        return addResult.IsSuccess
            ? Created()
            : BadRequest(addResult.ErrorMessage);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteMovieAsync(Guid id)
    {
        var deleteResult = await movieService.DeleteMovieAsync(id);
        
        return deleteResult.IsSuccess
            ? Ok()
            : NotFound(deleteResult.ErrorMessage);
    }
}