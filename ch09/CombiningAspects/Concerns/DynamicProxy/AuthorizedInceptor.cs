using Castle.DynamicProxy;

namespace CombiningAspects.Concerns.DynamicProxy;

public class AuthorizedInterceptor(IAuthorizationConcern authorizationConcern, string role) : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        var methodContext = invocation.ToMethodContext();
        authorizationConcern.OnEntry(methodContext, role);
        if (methodContext.Proceed)
            invocation.Proceed();
    }
}