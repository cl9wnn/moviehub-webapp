using API.Attributes;
using API.Models.Requests;
using API.Models.Responses;
using AutoMapper;
using Domain.Abstractions.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Admin;

[Route("api/admin/actors")]
[EntityExists<IActorService, Actor>]
[Authorize(Roles = "Admin")]
[ApiController]
public class AdminActorsController(IActorService actorService, IMediaService mediaService, IMapper mapper): ControllerBase
{
    [HttpGet("all")]
    public async Task<IActionResult> GetActorsAsync()
    {
        var getResult = await actorService.GetAllActorsAsync();
        
        var actors = mapper.Map<List<ActorResponse>>(getResult.Data);
        
        return Ok(actors);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetActorAsync(Guid id)
    {
        var getResult = await actorService.GetByIdAsync(id);
        
        var actor = mapper.Map<ActorResponse>(getResult.Data);
        
        return getResult.IsSuccess
            ? Ok(actor)
            : NotFound(getResult.ErrorMessage);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateActorAsync([FromBody] CreateActorRequest createActorRequest)
    {
        var actor = mapper.Map<Actor>(createActorRequest);
        var createResult = await actorService.CreateActorAsync(actor);
        
        return createResult.IsSuccess
            ? Created()
            : BadRequest(createResult.ErrorMessage);
    }
    
    [ValidateImageFile]
    [HttpPost("{id:guid}/portrait")]
    public async Task<IActionResult> UploadActorPortraitAsync(Guid id, IFormFile file)
    {
        var objectName = $"portraits/{id}{Path.GetExtension(file.FileName)}";
        const string bucketName = "actors";
        await using var stream = file.OpenReadStream();
        
        var urlResult = await mediaService.UploadMediaFile(stream, objectName, file.ContentType, bucketName);

        if (!urlResult.IsSuccess)
        {
            return BadRequest(urlResult.ErrorMessage);
        }
        
        var portraitUrl = urlResult.Data;
        
        var addOrUpdateResult = await actorService.AddOrUpdatePortraitPhotoAsync(portraitUrl, id);
        
        return addOrUpdateResult.IsSuccess 
            ? Ok()
            : BadRequest(addOrUpdateResult.ErrorMessage);
    }
    
    [ValidateImageFile]
    [HttpPost("{id:guid}/photo")]
    public async Task<IActionResult> UploadActorPhotoAsync(Guid id, IFormFile file)
    {
        var objectName = $"photos/{id}/{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        const string bucketName = "actors";
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
        
        var addResult = await actorService.AddActorPhotoAsync(photo, id);
        
        return addResult.IsSuccess 
            ? Ok()
            : BadRequest(urlResult.ErrorMessage);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteActorAsync(Guid id)
    {
        var deleteResult = await actorService.DeleteActorAsync(id);
        
        return deleteResult.IsSuccess
            ? Ok()
            : BadRequest(deleteResult.ErrorMessage);
    }
}