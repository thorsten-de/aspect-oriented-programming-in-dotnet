using Castle.DynamicProxy;

namespace CombiningAspects.Concerns.DynamicProxy;

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