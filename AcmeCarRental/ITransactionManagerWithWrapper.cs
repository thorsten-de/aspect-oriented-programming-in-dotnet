namespace AcmeCarRental;

/// <summary>
/// An internal interface that extends ITransactionManager to include a Wrapper method for executing actions within 
/// a transaction scope with retry logic. This allows for cleaner and more maintainable code in services that 
/// require transactional operations.
/// </summary>
public interface ITransactionManagerWithWrapper : ITransactionManager
{
    /// <summary>
    /// A wrapper method that executes the given action within a transaction scope. The transaction will be retried
    /// up to a maximum number of retries if it fails to complete successfully. After the maximum number of retries
    /// the exception will be re-thrown.
    /// </summary>
    /// <param name="action">Action to execute within a transaction scope.</param>
    void Wrapper(Action action)
    {
        using var scope = CreateScope();
        int retries = MaxRetries;
        bool succeeded = false;

        while (!succeeded)
        {
            try
            {
                action();
                succeeded = scope.Complete();
            }
            catch
            {
                if (retries > 0)
                    retries--;
                else
                    throw;
            }
        }
    }
}