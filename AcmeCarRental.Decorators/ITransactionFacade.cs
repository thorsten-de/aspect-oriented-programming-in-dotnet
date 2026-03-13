namespace AcmeCarRental.Decorators;

/// <summary>
/// Provides a facade for wrapping and executing actions with transaction management capabilities. These combine
/// both transaction and exception handling with retries
/// </summary>
public interface ITransactionFacade
{
    /// <summary>
    /// Wraps the provided action with transaction handling logic. This combines both the transaction management
    /// and exception handling with retries into a single method, allowing for cleaner and more maintainable code 
    /// in services that require transactional operations.
    /// </summary>
    /// <param name="action">The action to be executed within a transaction context.</param>
    void Wrapper(Action action);
}
