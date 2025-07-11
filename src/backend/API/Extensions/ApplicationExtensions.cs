﻿using API.Filters;
using API.Options;
using API.Pipeline.Middlewares;
using Hangfire;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

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
    
    public static IServiceProvider ApplyMigrations(this IServiceProvider services)
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
        
        return services;
    }

    public static WebApplication SeedDatabase(this WebApplication app)
    {
        MovieSeeder.SeedMovies(app.Services);
        return app;
    }
    
    public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder app)
    {
        app.UseMiddleware<RequestResponseLoggingMiddleware>();
        return app;
    }

    public static IApplicationBuilder UseHangfireDashboard(this IApplicationBuilder app)
    {
        app.UseHangfireDashboard("/hangfire", new DashboardOptions
        {
            Authorization = new[] { new AllowAllDashboardAuthorizationFilter() }
        });
        
        return app;
    }
    
    public static WebApplicationBuilder ConfigureSerilog(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<SensitiveLoggingOptions>(
            builder.Configuration.GetSection("SensitiveLogging"));
        
        var seqUrl = builder.Configuration["Seq:Url"];

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithThreadId()
            .WriteTo.Seq(seqUrl!)
            .Enrich.WithProperty("Application", "WebApp")
            .CreateLogger();
        
        builder.Host.UseSerilog();

        return builder;
    }
}