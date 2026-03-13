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
}
