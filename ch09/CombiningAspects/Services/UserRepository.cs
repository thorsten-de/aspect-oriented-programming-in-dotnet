namespace CombiningAspects.Services;

/// <summary>
/// Defines a repository for user-related data, such as roles.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Retrieves the roles for the current user.
    /// </summary>
    /// <returns>Set of roles</returns>
    HashSet<string> GetRolesForCurrentUser();
}

public class UserRepository : IUserRepository
{
    public HashSet<string> GetRolesForCurrentUser()
    {
        return ["Manager"];
    }
}