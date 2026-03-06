namespace AcmeCarRental;

/// <summary>
/// Represents a transaction scope that can be used to manage transactions in a consistent way. The scope should be 
/// disposed of after use, and if the Complete method is not called before disposal, the transaction will be rolled back.
/// This interface allows for different implementations of transaction management, such as using a database transaction 
/// or an in-memory transaction for testing purposes.
/// </summary>
public interface ITransactionScope : IDisposable
{
    /// <summary>
    /// Marks the transaction as successful. If this method is not called before the scope is disposed, 
    /// the transaction will be rolled back. 
    /// </summary>
    /// <returns>Should always return true, to set some success flag in the surrounding code if needed.</returns>
    bool Complete();
}
