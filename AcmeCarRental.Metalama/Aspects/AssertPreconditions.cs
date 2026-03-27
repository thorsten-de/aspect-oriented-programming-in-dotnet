using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System.Diagnostics;

namespace AcmeCarRental.Metalama.Aspects;


public class AssertPreconditionsAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        foreach (var p in meta.Target.Parameters)
        {
            ArgumentNullException.ThrowIfNull(p.Value, p.Name);
            if (p.Type.IsConvertibleTo(typeof(int)))
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero((int)p.Value!, p.Name);

        }

        return meta.Proceed();
    }
}
