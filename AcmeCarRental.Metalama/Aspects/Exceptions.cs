using AcmeCarRental.Data.Entities;

namespace AcmeCarRental.Metalama.Aspects;

/// <summary>
/// This is the base class for aspects responsible for handling exceptions thrown by the decorated service.
///  It uses an IExceptionHandler to determine whether an exception should be swallowed or re-thrown. This 
/// allows for centralized exception handling logic, such as logging or retrying, without cluttering the 
/// business logic of the service itself
/// </summary>
/// <param name="exceptionHandler">The exception handler to use for handling exceptions.</param>
internal abstract class ExceptionAspect(IExceptionHandler exceptionHandler)
{
    /// <summary>
    /// Wraps the provided action with exception handling logic. This method uses the IExceptionHandler to determine
    /// whether an exception should be handled and swallowed, or re-thrown. 
    /// </summary>
    /// <param name="action">The action to execute with exception handling.</param>
    protected void HandleExceptionsIn(Action action)
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            if (!exceptionHandler.Handle(ex))
                throw;
        }
    }
}

/// <summary>
/// This aspect is responsible for handling exceptions thrown by the ILoyaltyAccrualService. 
/// It is based on the ExceptionAspect, which provides the common exception handling logic, and implements 
/// the ILoyaltyAccrualService interface to be used as a decorator for the loyalty accrual service.
/// </summary>
/// <param name="service" >The ILoyaltyAccrualService to decorate.</param>
/// <param name="exceptionHandler">The IExceptionHandler to use for handling exceptions.</param
internal class AccureExceptionAspect(ILoyaltyAccrualService service, IExceptionHandler exceptionHandler)
: ExceptionAspect(exceptionHandler), ILoyaltyAccrualService
{
    /// <summary>
    /// Wraps the Accrue method of the ILoyaltyAccrualService with exception handling logic.
    /// </summary>
    /// <param name="agreement">The RentalAgreement for which to accrue loyalty points.</param>
    public void Accrue(RentalAgreement agreement) => HandleExceptionsIn(() => service.Accrue(agreement));
}

/// <summary>
/// This aspect is responsible for handling exceptions thrown by the ILoyaltyRedeemService. 
/// It is based on the ExceptionAspect, which provides the common exception handling logic, and implements 
/// the ILoyaltyRedeemService interface to be used as a decorator for the loyalty redeem service.
/// </summary>
/// <param name="service" >The ILoyaltyRedeemService to decorate.</param>
/// <param name="exceptionHandler">The IExceptionHandler to use for handling exceptions.</param>
internal class RedeemExceptionAspect(ILoyaltyRedeemService service, IExceptionHandler exceptionHandler)
 : ExceptionAspect(exceptionHandler), ILoyaltyRedeemService
{
    /// <summary>
    /// Wraps the Redeem method of the ILoyaltyRedeemService with exception handling logic.
    /// </summary>
    /// <param name="invoice">The Invoice for which to redeem loyalty points.</param>
    /// <param name="numberOfDays">The number of days for which to redeem loyalty points.</param>
    public void Redeem(Invoice invoice, int numberOfDays) => HandleExceptionsIn(() => service.Redeem(invoice, numberOfDays));
}