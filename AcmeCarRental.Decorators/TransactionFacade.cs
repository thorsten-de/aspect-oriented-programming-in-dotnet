namespace AcmeCarRental.Decorators;

/// <summary>
/// The TransactionFacade combines the transaction management and exception handling with retries into a single method
/// </summary>
/// <param name="transactions">Transaction manager to use for managing transactions and retries.</param>
/// <param name="exceptionHandler">Exception handler to use for handling exceptions</param>
public class TransactionFacade(ITransactionManager transactions, IExceptionHandler exceptionHandler) : ITransactionFacade
{
    /// <summary>
    /// The Wrapper methods combines the ExceptionHandler and TransactionManager wrapper methods into a single method
    /// </summary>
    /// <param name="action">The action to execute within the transaction and exception handling context</param>
    public void Wrapper(Action action)
        => exceptionHandler.Wrapper(() => transactions.Wrapper(action));
}
