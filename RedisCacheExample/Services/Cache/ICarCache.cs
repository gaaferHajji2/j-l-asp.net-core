namespace RedisCacheExample.Services.Cache;

public interface ICarCache
{
    T? GetData<T>(string key);
    void SetData<T>(string key, T value);
    void InvalidateCache(string key);
}
