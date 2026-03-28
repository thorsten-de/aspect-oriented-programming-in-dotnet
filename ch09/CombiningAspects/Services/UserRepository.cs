namespace CombiningAspects.Services;

public interface IUserRepository
{
    HashSet<string> GetRolesForCurrentUser();
}

public class UserRepository : IUserRepository
{
    public HashSet<string> GetRolesForCurrentUser()
    {
        return ["Manager"];
    }
}