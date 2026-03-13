using System;

namespace AcmeCarRental;

/// <summary>
/// Represents a handler for exceptions that occur during the execution of service methods.
/// </summary>
public interface IExceptionHandler
{
    /// <summary>
    /// Handles an exception that occurred during the execution of a service method. The handler can choose 
    /// to handle the exception and return true, or to not handle the exception and return false,
    ///  in which case the exception is supposed to be re-thrown.
    /// </summary>
    /// <param name="ex">The exception to handle.</param>
    /// <returns>True if the exception was handled and should not be re-thrown, false if the exception should be re-thrown.</returns>
    bool Handle(Exception ex);

}
