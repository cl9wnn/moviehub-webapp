using Domain.Abstractions.Services;
using Hangfire;
using Hangfire.PostgreSql;
using Infrastructure.BackgroundJobs;
using Infrastructure.Cache;
using Infrastructure.Database;
using Infrastructure.Email;
using Infrastructure.FileStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPostgresDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("PostgresDbConnection"));
        });

        return services;
    }
    
    public static IServiceCollection AddMinioStorage(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MinioOptions>(configuration.GetSection("MinioOptions"));

        services.AddSingleton<IFileStorageService, MinioFileStorageService>();

        return services;
    }
    
    public static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RedisOptions>(configuration.GetSection("RedisOptions"));
        
        var redisOptions = configuration.GetSection("RedisOptions").Get<RedisOptions>()!;

        var redisConfiguration = $"{redisOptions.Host}:{redisOptions.Port}";
        
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisConfiguration;
        });
        
        services.AddSingleton<IDistributedCacheService, RedisCacheService>();
        
        services.AddSingleton<IConnectionMultiplexer>(
            ConnectionMultiplexer.Connect(redisConfiguration));

        return services;
    }

    public static IServiceCollection AddHangfire(this IServiceCollection services, IConfiguration config)
    {
        services.AddHangfire(configuration =>
        {
            configuration.UsePostgreSqlStorage(options =>
            {
                options.UseNpgsqlConnection(config.GetConnectionString("PostgresDbConnection"));
            });
        });
        
        services.AddHangfireServer();
        
        return services;
    }

    public static IServiceCollection AddEmailService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailOptions>(configuration.GetSection("EmailOptions"));
        
        services.AddTransient<IEmailService, EmailService>();
        
        return services;
    }
}