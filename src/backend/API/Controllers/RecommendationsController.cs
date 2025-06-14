using API.Extensions;
using API.Models.Responses;
using AutoMapper;
using Domain.Abstractions.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RecommendationsController(IRecommendationService recommendationService, IMapper mapper): ControllerBase
{
    [HttpGet("user-movies")]
    public async Task<IActionResult> GetMovieRecommendationsByUserIdAsync()
    {
        if (User.GetUserId() is not Guid userId)
        {
            return Unauthorized("Incorrect format for user id");
        }

        var getResult = await recommendationService.GetMovieRecommendationsByUserAsync(userId);
        
        var movies = mapper.Map<List<RecommendationMovieResponse>>(getResult.Data);
        
        return getResult.IsSuccess
            ? Ok(movies)
            : BadRequest(getResult.ErrorMessage);
    }
    
    [AllowAnonymous]
    [HttpGet("top-movies")]
    public async Task<IActionResult> GetMovieRecommendationsByUserRatingAsync()
    {
        var getResult = await recommendationService.GetMovieRecommendationsByRatingAsync();
        
        var movies = mapper.Map<List<RecommendationMovieResponse>>(getResult.Data);
        
        return getResult.IsSuccess
            ? Ok(movies)
            : BadRequest(getResult.ErrorMessage);
    }
}