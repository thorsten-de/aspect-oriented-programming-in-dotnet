namespace CombiningAspects.Services;

/// <summary>
/// Defines the contract for a cache service, which allows storing and retrieving cached values based on a string key.
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// Gets or sets a cached value for the given key. 
    /// </summary>
    /// <param name="cacheKey">The key to identify the cached value.</param>
    /// <returns>The cached value associated with the key.</returns>
    object this[string cacheKey] { get; set; }

    /// <summary>
    /// Checks if the cache contains a value for the given key.
    /// </summary>
    /// <param name="cacheKey">The key to check in the cache.</param>
    /// <returns>True if the cache contains the key, otherwise false.</returns>
    bool ContainsKey(string cacheKey);
}

/// <summary>
/// A simple implementation of the ICacheService interface using an in-memory dictionary to store cached values.
/// </summary>
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