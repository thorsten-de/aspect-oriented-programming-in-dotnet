using Castle.DynamicProxy;

namespace CombiningAspects.Concerns.DynamicProxy;

/// <summary>
/// An interceptor that applies caching logic using the provided ICachingConcern. 
/// </summary>
/// <param name="cachingConcern">The caching concern that contains the caching logic to be applied.</param>
public class CachedInterceptor(ICachingConcern cachingConcern) : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        var methodContext = invocation.ToMethodContext();
        cachingConcern.OnEntry(methodContext);
        if (!methodContext.Proceed)
            return;
        invocation.Proceed();
        cachingConcern.OnSuccess(methodContext);
    }
}