using System.Reflection;
using PostSharp.Aspects;
using PostSharp.Aspects.Dependencies;
using PostSharp.Serialization;

namespace CombiningAspects.Concerns.PostSharp;

/// <summary>
/// Ensures that the current user has the specified role before allowing method execution to proceed.
/// </summary>
/// <remarks>
/// This aspect should be applied before caching aspects to ensure that authorization checks are performed
/// before any cached results are returned.
/// <remarks>
/// <param name="role">The required role for method execution.</param>
[PSerializable]
[AspectRoleDependency(AspectDependencyAction.Order, AspectDependencyPosition.Before, StandardRoles.Caching)]
public class AuthorizedAttribute(string role) : OnMethodBoundaryAspect
{
    [PNonSerialized]
    private IAuthorizationConcern _authConcern = null!;

    public override void RuntimeInitialize(MethodBase method) =>
        _authConcern = ObjectFactory.GetInstance<IAuthorizationConcern>();

    public override void OnEntry(MethodExecutionArgs args) =>
        _authConcern.OnEntry(args.ToMethodContext(), role);
}

