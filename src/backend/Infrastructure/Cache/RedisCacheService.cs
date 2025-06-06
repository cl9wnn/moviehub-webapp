using System.Text.Json;
using System.Text.Json.Serialization;
using Domain.Abstractions.Services;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Cache;

public class RedisCacheService(IDistributedCache cache, ILogger<RedisCacheService> logger) : IDistributedCacheService
{
    private readonly IDistributedCache _cache = cache;
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        ReferenceHandler = ReferenceHandler.Preserve,
        WriteIndented = false
    };

    public async Task<T?> GetAsync<T>(string key)
    {
        var cacheData = await _cache.GetStringAsync(key);

        if (string.IsNullOrEmpty(cacheData))
        {
            return default;
        }

        try
        {
            return JsonSerializer.Deserialize<T>(cacheData, _jsonSerializerOptions);
        }
        catch (Exception ex)
        {
            logger.LogError("Error parsing cache: {@ex}", ex);
            return default;
        }
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
    {
        var serializeData = JsonSerializer.Serialize(value, _jsonSerializerOptions);
        var options = new DistributedCacheEntryOptions();
        
        if (expiry.HasValue)
        {
            options.SetSlidingExpiration(expiry.Value);
        }
        
        await _cache.SetStringAsync(key, serializeData, options);
    }

    public async Task RemoveAsync(string key)
    {
        await _cache.RemoveAsync(key);
    }
}