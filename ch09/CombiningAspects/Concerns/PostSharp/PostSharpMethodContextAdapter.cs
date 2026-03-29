using PostSharp.Aspects;

namespace CombiningAspects.Concerns.PostSharp;

public static class PostSharpMethodContextExtensions
{
    /// <summary>
    /// Adapts PostSharp's MethodExecutionArgs to our IMethodContextAdapter interface.
    /// </summary>
    private class PostSharpMethodContextAdapter(MethodExecutionArgs args) : IMethodContextAdapter
    {
        public bool Proceed => args.FlowBehavior == FlowBehavior.Continue;

        public object Tag
        {
            get => args.MethodExecutionTag;
            set => args.MethodExecutionTag = value;
        }

        public object? ReturnValue
        {
            get => args.ReturnValue;
            set => args.ReturnValue = value;
        }

        public string MethodName => args.Method.Name;

        public object?[] Arguments => args.Arguments.ToArray();

        public void AbortMethod()
        {
            args.FlowBehavior = FlowBehavior.Return;
        }

    }

    /// <summary>
    /// Converts PostSharp's MethodExecutionArgs to our IMethodContextAdapter
    /// </summary>
    public static IMethodContextAdapter ToMethodContext(this MethodExecutionArgs args) =>
        new PostSharpMethodContextAdapter(args);
}