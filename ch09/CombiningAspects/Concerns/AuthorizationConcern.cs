using CombiningAspects.Services;

namespace CombiningAspects.Concerns;

/// <summary>
/// Defines the contract for authorization behavior. This concern will be used 
/// by our AuthorizedAttribute to implement authorization logic.
/// </summary>
public interface IAuthorizationConcern
{
    void OnEntry(IMethodContextAdapter methodContext, string role);
}

/// <summary>
/// Implements authorization behavior for methods decorated with the AuthorizedAttribute. 
/// It checks the user's roles on method entry and throws an exception if unauthorized.
/// </summary>
/// <param name="users">The user repository used to retrieve the current user's roles.</param>
public class AuthorizationConcern(IUserRepository users) : IAuthorizationConcern
{
    public void OnEntry(IMethodContextAdapter methodContext, string role)
    {
        Console.WriteLine($"[Auth] Checking if user is in {role} role");
        if (UserIsInRole(role))
        {
            Console.WriteLine("[Auth] User IS authorized");
            return;
        }

        Console.WriteLine("[Auth] User is NOT authorized");
        UnauthorizedAccess();
    }

    private bool UserIsInRole(string role)
    {
        var roles = users.GetRolesForCurrentUser();
        return roles.Contains(role);
    }

    private void UnauthorizedAccess()
    {
        throw new UnauthorizedAccessException("Access denied");
    }
}