using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationExtensions
{
    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{env.ApplicationName} v1");
            c.RoutePrefix = string.Empty;
        });
        
        return app;
    }
    
    public static void ApplyMigrations(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        try
        {
            var context = serviceProvider.GetRequiredService<AppDbContext>();
            context.Database.Migrate();
        }
        catch (Exception ex)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "Ошибка при применении миграций.");
        }
    }
}