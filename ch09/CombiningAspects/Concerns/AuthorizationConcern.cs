using CombiningAspects.Services;

namespace CombiningAspects.Concerns;

public interface IAuthorizationConcern
{
    void OnEntry(IMethodContextAdapter methodContext, string role);
}

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