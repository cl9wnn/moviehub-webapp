using Domain.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters;

public class EntityExistsFilter<TService, TDto>(TService service, string paramName): IAsyncActionFilter
    where TService : class, IEntityService<TDto>
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.ActionArguments.TryGetValue(paramName, out var idObj) && idObj is Guid id)
        {
            var existsResult = await service.ExistsAsync(id);
            
            if (!existsResult.IsSuccess)
            {
                context.Result = new NotFoundObjectResult(new {Error = existsResult.ErrorMessage});
                return;
            }
        }
        
        await next();
    }
}