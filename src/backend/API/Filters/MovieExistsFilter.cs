using Domain.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters;

public class MovieExistsFilter(IMovieService movieService): IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.ActionArguments.TryGetValue("id", out var movieIdObj) && movieIdObj is Guid id)
        {
            var getResult = await movieService.GetMovieAsync(id);
            if (!getResult.IsSuccess)
            {
                context.Result = new NotFoundObjectResult(getResult.ErrorMessage);
                return;
            }
        }
        
        await next();
    }
}