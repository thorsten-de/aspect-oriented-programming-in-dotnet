using System.Reflection;
using PostSharp.Aspects;
using PostSharp.Aspects.Dependencies;
using PostSharp.Serialization;

namespace CombiningAspects.Concerns.PostSharp;

[PSerializable]
[ProvideAspectRole(StandardRoles.Caching)]
public class CachedAttribute : OnMethodBoundaryAspect
{
    [PNonSerialized]
    private ICachingConcern _cacheConcern = null!;

    public override void RuntimeInitialize(MethodBase method) =>
        _cacheConcern = ObjectFactory.GetInstance<ICachingConcern>();

    public override void OnEntry(MethodExecutionArgs args) =>
        _cacheConcern.OnEntry(args.ToMethodContext());

    public override void OnSuccess(MethodExecutionArgs args) =>
        _cacheConcern.OnSuccess(args.ToMethodContext());
}
