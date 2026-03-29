namespace CombiningAspects.Concerns;

/// <summary>
/// Defines a common interface for method context adapters, allowing different AOP frameworks 
/// to adapt their method context to a unified interface used by our concerns.
/// </summary>
public interface IMethodContextAdapter
{
    object? Tag { get; set; }
    object? ReturnValue { get; set; }
    string MethodName { get; }
    object?[] Arguments { get; }
    void AbortMethod();

    bool Proceed { get; }
}
