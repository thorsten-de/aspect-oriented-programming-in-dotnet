using System.Reflection;
using PostSharp.Aspects;
using PostSharp.Aspects.Dependencies;
using PostSharp.Serialization;

namespace CombiningAspects.Concerns.PostSharp;

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

