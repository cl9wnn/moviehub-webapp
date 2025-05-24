using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters;

public class ActorExistsFilter(IActorService actorService): IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.ActionArguments.TryGetValue("id", out var actorIdObj) && actorIdObj is Guid id)
        {
            var getResult = await actorService.GetActorAsync(id);
            if (!getResult.IsSuccess)
            {
                context.Result = new NotFoundObjectResult(getResult.ErrorMessage);
                return;
            }
        }
        
        await next();
    }
}