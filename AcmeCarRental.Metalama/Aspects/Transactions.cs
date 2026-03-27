using Metalama.Extensions.DependencyInjection;
using Metalama.Framework.Aspects;

namespace AcmeCarRental.Metalama.Aspects;

/// <summary>
/// This aspect is responsible for managing transactions around the decorated service. It uses an ITransactionManager to create a transaction scope
/// and manage retries in case of transient failures. This allows for centralized transaction management logic, such as retry policies, without cluttering
/// the business logic of the service itself.
/// </summary>
public class TransactionManagementAttribute : OverrideMethodAspect
{
    /// <summary>
    /// Gets the transaction manager instance to be used for managing transactions. This is a dependency injected by the framework somehow
    /// </summary>
    [IntroduceDependency]
    private readonly ITransactionManager _transactionManager = null!;

    /// <summary>
    /// Number of retries to attempt in case of transient failures. This can be set when decorating 
    /// the service, allowing for different retry policies for different instances if needed.
    /// </summary>
    public int MaxRetries { get; set; } = 3;

    /// <summary>
    /// Wraps the provided action with transaction handling logic. This method creates a transaction scope 
    /// using the ITransactionManager, and manages retries in case of transient failures. If the action 
    /// fails after the maximum number of retries, the exception will be re-thrown.
    /// </summary>
    public override dynamic? OverrideMethod()
    {
        using var scope = _transactionManager.CreateScope();
        int retries = meta.RunTime(MaxRetries);
        bool succeeded = false;

        while (!succeeded)
        {
            try
            {
                var result = meta.Proceed();
                succeeded = scope.Complete();
                return result;
            }
            catch
            {
                if (retries > 0)
                    retries--;
                else
                    throw;
            }
        }

        return null;
    }
}