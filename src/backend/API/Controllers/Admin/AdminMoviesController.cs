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

namespace API.Controllers.Admin;

[Route("api/admin/movies")]
[EntityExists<IMovieService, Movie>]
[Authorize(Roles = "Admin")]
[ApiController]
public class AdminMoviesController(IMovieService movieService, IMediaService mediaService, IMapper mapper): ControllerBase
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
        var getResult = await movieService.GetByIdAsync(id);
        
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
    
    [ValidateImageFile]
    [HttpPost("{id:guid}/poster")]
    public async Task<IActionResult> UploadMoviePoster(Guid id, IFormFile? file)
    {
        var objectName = $"posters/{id}{Path.GetExtension(file.FileName)}";
        const string bucketName = "movies";
        await using var stream = file.OpenReadStream();
        
        var urlResult = await mediaService.UploadMediaFile(stream, objectName, file.ContentType, bucketName);
        
        if (!urlResult.IsSuccess)
        {
            return BadRequest(urlResult.ErrorMessage);
        }
        
        var posterUrl = urlResult.Data;
        
        var addOrUpdateResult = await movieService.AddOrUpdatePosterPhotoAsync(posterUrl, id);
        
        return addOrUpdateResult.IsSuccess 
            ? Ok()
            : BadRequest(addOrUpdateResult.ErrorMessage);
    }
    
    [ValidateImageFile]
    [HttpPost("{id:guid}/photo")]
    public async Task<IActionResult> UploadMoviePhotoAsync(Guid id, IFormFile? file)
    {
        var objectName = $"photos/{id}/{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        const string bucketName = "movies";
        await using var stream = file.OpenReadStream();
        
        var urlResult = await mediaService.UploadMediaFile(stream, objectName, file.ContentType, bucketName);

        if (!urlResult.IsSuccess)
        {
            return BadRequest(urlResult.ErrorMessage);
        }
        
        var photo = new Photo
        {
            ImageUrl = urlResult.Data,
        };
        
        var addResult = await movieService.AddMoviePhotoAsync(photo, id);
        
        return addResult.IsSuccess 
            ? Ok()
            : BadRequest(urlResult.ErrorMessage);
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