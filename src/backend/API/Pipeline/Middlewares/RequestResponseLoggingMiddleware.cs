using System.Text.Json;
using System.Text.Json.Nodes;
using API.Options;
using Microsoft.Extensions.Options;

namespace API.Pipeline.Middlewares;

public class RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger,
    IOptionsMonitor<SensitiveLoggingOptions> options)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var sensitiveKeys = new HashSet<string>(options.CurrentValue.Keys, StringComparer.OrdinalIgnoreCase);
        
        var isMedia = context.Request.ContentType?.StartsWith("multipart/form-data", StringComparison.OrdinalIgnoreCase) == true;
        var requestBody = "[Skipped due to multipart/form-data]";
        
        if (!isMedia)
        {
            context.Request.EnableBuffering();
            using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
            requestBody = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;
        }
        
        var originalBodyStream = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        await next(context);
        
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        
        var filteredRequestBody = FilterSensitiveData(requestBody, sensitiveKeys);
        var filteredResponseBody = FilterSensitiveData(responseText, sensitiveKeys);
        
        logger.LogInformation("HTTP  {Method}  {Path}  {StatusCode} Request: {@RequestBody} Response: {@ResponseBody}", 
            context.Request.Method, context.Request.Path, context.Response.StatusCode, filteredRequestBody, filteredResponseBody);
        
        await responseBody.CopyToAsync(originalBodyStream);
    }
    
    private string FilterSensitiveData(string json, HashSet<string> sensitiveKeys)
    {
        try
        {
            var node = JsonNode.Parse(json);
            if (node is not null)
                FilterNode(node, sensitiveKeys);
            return node?.ToJsonString(new JsonSerializerOptions
            {
                WriteIndented = false,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping

            }) ?? json;
        }
        catch
        {
            return json;
        }
    }

    private void FilterNode(JsonNode node, HashSet<string> sensitiveKeys)
    {
        if (node is JsonObject obj)
        {
            foreach (var key in obj.ToList())
            {
                if (sensitiveKeys.Contains(key.Key))
                {
                    obj[key.Key] = "[PROTECTED]";
                }
                else if (obj[key.Key] is JsonNode childNode)
                {
                    FilterNode(childNode, sensitiveKeys);
                }
            }
        }
        else if (node is JsonArray array)
        {
            foreach (var item in array)
            {
                if (item is not null)
                    FilterNode(item, sensitiveKeys);
            }
        }
    }
}