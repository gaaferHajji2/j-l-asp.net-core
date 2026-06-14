using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace RedisCacheExample.Services.Cache;

public class CarCache : ICarCache
{

    private readonly IDistributedCache? _cache;

    public CarCache(IDistributedCache cache)
    {
        _cache = cache;
    }

    public T? GetData<T>(string key)
    {
        var data = _cache?.GetString(key);

        if (data is null)
        {
            return default(T);
        }

        return JsonSerializer.Deserialize<T>(data);
    }

    public void InvalidateCache(string key)
    {
        _cache?.Remove("cars");
        _cache?.Remove(key);
    }

    public void SetData<T>(string key, T value)
    {
        var options = new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
        };

        _cache?.SetString(key, JsonSerializer.Serialize(value), options);
    }
}
