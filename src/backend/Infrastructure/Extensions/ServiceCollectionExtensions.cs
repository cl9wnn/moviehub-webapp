using Domain.Abstractions.Services;
using Infrastructure.Cache;
using Infrastructure.Database;
using Infrastructure.FileStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            options.InstanceName = "MovieHub:"; 
        });
        
        services.AddSingleton<IDistributedCacheService, RedisCacheService>();

        return services;
    }
}