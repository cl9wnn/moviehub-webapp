using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters;

public class ValidateImageFileFilter: IAsyncActionFilter
{
    private readonly string[] _allowedExtensions = ["image/jpeg", "image/png", "image/webp"];
    private readonly long _maxFileSizeBytes = 10 * 1024 * 1024;

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ActionArguments.TryGetValue("file", out var fileObj) || fileObj is not IFormFile file
                                                                          || file.Length == 0)
        {
            context.Result = new BadRequestObjectResult("File is required!");
            return;
        }
        
        if (file.Length > _maxFileSizeBytes)
        {
            context.Result = new BadRequestObjectResult("File is too big (> 10 MB)!");
            return;
        }

        if (!_allowedExtensions.Contains(file.ContentType))
        {
            context.Result = new BadRequestObjectResult($"Unsupported file format!: {file.ContentType}");
            return;
        }
        
        await next();
    }
}