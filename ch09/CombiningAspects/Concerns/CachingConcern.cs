using System.Text;
using CombiningAspects.Services;

namespace CombiningAspects.Concerns;


/// <summary>
/// Defines the contract for caching behavior. This concern will be used by our CachedAttribute to implement caching logic.
/// </summary>
public interface ICachingConcern
{
    void OnEntry(IMethodContextAdapter methodContext);
    void OnSuccess(IMethodContextAdapter methodContext);
}

/// <summary>
/// Implements caching behavior for methods decorated with the CachedAttribute. 
/// It checks the cache on method entry and stores results on success.
/// </summary>
/// <param name="cache">The cache service used to store and retrieve cached values.</param>
public class CachingConcern(ICacheService cache) : ICachingConcern
{
    public void OnEntry(IMethodContextAdapter methodContext)
    {
        var cacheKey = BuildCacheKey(methodContext);
        if (!cache.ContainsKey(cacheKey))
        {
            Console.WriteLine($"[Cache] MISS for {cacheKey}");
            methodContext.Tag = cacheKey;
            return;
        }
        Console.WriteLine($"[Cache] HIT for {cacheKey}");
        methodContext.ReturnValue = cache[cacheKey];
        methodContext.AbortMethod();
    }

    public void OnSuccess(IMethodContextAdapter methodContext)
    {
        var cacheKey = (string)methodContext.Tag;
        Console.WriteLine($"[Cache] Storing value for {cacheKey}");
        cache[cacheKey] = methodContext.ReturnValue;
    }

    private string BuildCacheKey(IMethodContextAdapter methodContext)
    {
        var key = new StringBuilder(methodContext.MethodName);
        foreach (var arg in methodContext.Arguments)
            key.Append("_").Append(arg.ToString());

        return key.ToString();
    }
}