namespace CombiningAspects.Services;

public interface ICacheService
{
    object this[string cacheKey] { get; set; }
    bool ContainsKey(string cacheKey);
}

public class CacheService : ICacheService
{
    private readonly Dictionary<string, object> _cache = new();

    public object this[string cacheKey]
    {
        get => _cache[cacheKey];
        set => _cache[cacheKey] = value;
    }

    public bool ContainsKey(string cacheKey) => _cache.ContainsKey(cacheKey);
}