namespace CombiningAspects.Concerns;

/// <summary>
/// Defines a common interface for method context adapters, allowing different AOP frameworks 
/// to adapt their method context to a unified interface used by our concerns.
/// </summary>
public interface IMethodContextAdapter
{
    /// <summary>
    /// Gets or sets a tag object that can be used to store arbitrary data related to the method execution.
    /// </summary>
    object? Tag { get; set; }

    /// <summary>
    /// Gets or sets the return value of the method. This allows concerns to modify the return value
    /// after the method execution or to set a return value when aborting the method execution.
    /// </summary>
    object? ReturnValue { get; set; }

    /// <summary>
    /// Gets the name of the method being executed. This allows concerns to identify which method 
    /// is being intercepted and apply logic accordingly.
    /// </summary>
    string MethodName { get; }

    /// <summary>
    /// Gets the arguments passed to the method. This allows concerns to inspect or modify the 
    /// arguments before the method execution.
    /// </summary>
    object?[] Arguments { get; }

    /// <summary>
    /// Aborts the method execution, preventing the original method from being called. This allows concerns 
    /// to stop the execution of the method based on certain conditions.
    /// </summary>
    void AbortMethod();

    /// <summary>
    /// Indicates whether the method execution should proceed. This allows concerns to control 
    /// the flow of method execution.
    /// </summary>
    bool Proceed { get; }
}
