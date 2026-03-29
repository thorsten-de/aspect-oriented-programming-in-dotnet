using Castle.DynamicProxy;

namespace CombiningAspects.Concerns.DynamicProxy;

public static class MethodContextExtensions
{
    public static IMethodContextAdapter ToMethodContext(this IInvocation invocation) =>
        new CdpMethodContextAdapter(invocation);


    private class CdpMethodContextAdapter(IInvocation invocation) : IMethodContextAdapter
    {
        public bool Proceed { get; private set; } = true;

        public object? Tag { get; set; }

        public object? ReturnValue
        {
            get => invocation.ReturnValue;
            set => invocation.ReturnValue = value;
        }

        public string MethodName => invocation.Method.Name;

        public object?[] Arguments => invocation.Arguments;

        public void AbortMethod()
        {
            Proceed = false;
        }

    }
}