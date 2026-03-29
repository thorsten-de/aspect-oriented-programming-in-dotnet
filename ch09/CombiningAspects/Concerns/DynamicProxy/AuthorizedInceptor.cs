using Castle.DynamicProxy;

namespace CombiningAspects.Concerns.DynamicProxy;

/// <summary>
/// An interceptor that applies authorization logic using the provided IAuthorizationConcern and role.
/// </summary>
/// <param name="authorizationConcern">The authorization concern that contains the authorization logic to be applied.</param>
/// <param name="role">The role to check for authorization.</param>
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