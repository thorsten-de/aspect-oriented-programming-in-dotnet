namespace AcmeCarRental;

/// <summary>
/// The IExceptionHandleWithWrapper interface extends the IExeptionHandler interface by adding a Wrapper method 
/// that executes a given action and handles any exceptions that occur during its execution using the Handle method.
/// f an exception occurs and is not handled, it will be re-thrown. This allows for cleaner and more maintainable 
/// code in services that require exception handling.
/// </summary>
public interface IExceptionHandlerWithWrapper : IExceptionHandler
{
    /// <summary>
    /// A wrapper method that executes the given action and handles any exceptions that occur during its execution
    /// using the Handle method. If an exception occurs and is not handled, it will be re-thrown.
    /// </summary>
    /// <param name="action">Action to execute and handle exceptions for.</param>
    void Wrapper(Action action)
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            if (!Handle(ex))
                throw;
        }
    }
}