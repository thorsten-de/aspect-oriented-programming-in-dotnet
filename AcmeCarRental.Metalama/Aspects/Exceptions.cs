using Metalama.Extensions.DependencyInjection;
using Metalama.Framework.Aspects;

namespace AcmeCarRental.Metalama.Aspects;


/// <summary>
/// This is the aspect responsible for handling exceptions thrown by the decorated service.
/// It uses an IExceptionHandler to determine whether an exception should be swallowed or re-thrown. This 
/// allows for centralized exception handling logic, such as logging or retrying, without cluttering the 
/// business logic of the service itself
/// </summary>
public class ExceptionHandlingAttribute : OverrideMethodAspect
{
    /// <summary>
    /// The exception handler to use for handling exceptions.
    /// </summary>
    [IntroduceDependency]
    public IExceptionHandler ExceptionHandler { get; set; }

    /// <summary>
    /// Wraps the provided action with exception handling logic. This method uses the IExceptionHandler to determine
    /// whether an exception should be handled and swallowed, or re-thrown. 
    /// </summary>
    /// <param name="action">The action to execute with exception handling.</param>
    public override dynamic? OverrideMethod()
    {
        var handler = ExceptionHandler;

        try
        {
            return meta.Proceed();
        }
        catch (Exception ex)
        {
            if (!handler.Handle(ex))
                throw;
        }
        return null;
    }
}