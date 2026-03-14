using AcmeCarRental.Data.Entities;

namespace AcmeCarRental.Metalama.Aspects;

/// <summary>
/// This is the base class for aspects responsible for handling transactions around the decorated service. 
/// It uses an ITransactionManager to create a transaction scope and manage retries in case of transient 
/// failures. This allows for centralized transaction management logic, such as retry policies, 
/// without cluttering the business logic of the service itself.
/// </summary>
/// <param name="transactionManager">The transaction manager to use for managing transactions.</param>
internal class TransactionAspect(ITransactionManager transactionManager)
{
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
    /// <param name="action">The action to execute within a transaction scope.</param>
    protected void WrapTransactionAround(Action action)
    {
        using var scope = transactionManager.CreateScope();
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


/// <summary>
/// This aspect is responsible for handling transactions around the ILoyaltyAccrualService. 
/// It is based on the TransactionAspect, which provides the common transaction handling logic, and implements
/// the ILoyaltyAccrualService interface to be used as a decorator for the loyalty accrual service.
/// </summary>
/// <param name="service" >The ILoyaltyAccrualService to decorate.</param>
/// <param name="transactionManager">The ITransactionManager to use for managing transactions.</param>
internal class AccrualTransactionAspect(ILoyaltyAccrualService service, ITransactionManager transactionManager)
   : TransactionAspect(transactionManager), ILoyaltyAccrualService
{
    /// <summary>
    /// Wraps the Accrue method of the ILoyaltyAccrualService with transaction handling logic.
    /// </summary>
    /// <param name="agreement">The RentalAgreement for which to accrue loyalty points.</param>
    public void Accrue(RentalAgreement agreement) => WrapTransactionAround(() => service.Accrue(agreement));
}

/// <summary>
/// This aspect is responsible for handling transactions around the ILoyaltyRedeemService. 
/// It is based on the TransactionAspect, which provides the common transaction handling logic, and implements
/// the ILoyaltyRedeemService interface to be used as a decorator for the loyalty redeem service.
/// </summary>
/// <param name="service" >The ILoyaltyRedeemService to decorate.</param>
/// <param name="transactionManager">The ITransactionManager to use for managing transactions.</param>
internal class RedeemTransactionAspect(ILoyaltyRedeemService service, ITransactionManager transactionManager)
    : TransactionAspect(transactionManager), ILoyaltyRedeemService
{
    /// <summary>
    /// Wraps the Redeem method of the ILoyaltyRedeemService with transaction handling logic.
    /// </summary>
    /// <param name="invoice">The Invoice for which to redeem loyalty points.</param>
    /// <param name="numberOfDays">The number of days for which to redeem loyalty points.</param>
    public void Redeem(Invoice invoice, int numberOfDays) => WrapTransactionAround(() => service.Redeem(invoice, numberOfDays));
}
