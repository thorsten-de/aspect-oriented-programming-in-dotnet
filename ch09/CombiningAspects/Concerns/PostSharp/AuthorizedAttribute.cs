using System.Reflection;
using PostSharp.Aspects;
using PostSharp.Serialization;

namespace CombiningAspects.Concerns.PostSharp;

[PSerializable]
public class AuthorizedAttribute(string role) : OnMethodBoundaryAspect
{
    [PNonSerialized]
    private IAuthorizationConcern _authConcern = null!;

    public override void RuntimeInitialize(MethodBase method) =>
        _authConcern = ObjectFactory.GetInstance<IAuthorizationConcern>();

    public override void OnEntry(MethodExecutionArgs args) =>
        _authConcern.OnEntry(args.ToMethodContext(), role);
}

