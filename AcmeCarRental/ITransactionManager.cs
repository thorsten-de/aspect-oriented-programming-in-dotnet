namespace AcmeCarRental;


/// <summary>
/// Represents a transaction manager that can create transaction scopes. This allows for consistent transaction management across different services, and enables the use of different 
/// transaction management implementations, such as using a database transaction or an in-memory transaction for testing purposes.
/// </summary>
public interface ITransactionManager
{
    const int MaxRetries = 3;
    /// <summary>
    /// Creates a new transaction scope. The scope should be disposed of after use, and if the Complete 
    /// method is not called before disposal, the transaction will be rolled back.
    /// </summary>
    /// <returns>A new transaction scope.</returns>
    ITransactionScope CreateScope();

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
