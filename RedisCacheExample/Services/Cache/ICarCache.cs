namespace RedisCacheExample.Services.Cache;

public interface ICarCache
{
    T? GetData<T>(string key);
    void setData<T>(string key, T value);
}
