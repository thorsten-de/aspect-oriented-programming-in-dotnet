using AcmeCarRental.Data;
using AcmeCarRental.Data.Entities;
using Microsoft.Extensions.Logging;

namespace AcmeCarRental;

internal class LoyaltyRedeemService(
    ILoyaltyDataService loyaltyDataService,
    ILogger logger,
    ITransactionManager transactions,
    IExceptionHandler exceptionHandler
    ) : ILoyaltyRedeemService
{
    private const int MaxRetries = 3;
    public void Redeem(Invoice invoice, int numberOfDays)
    {
        #region Defensive programming
        ArgumentNullException.ThrowIfNull(invoice);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(numberOfDays);
        #endregion

        #region Logging
        logger.LogInformation("Redeem: {date}", DateTime.Now);
        logger.LogInformation("Invoice: {invoiceId}", invoice.Id);
        #endregion

        try
        {
            using var scope = transactions.CreateScope();

            int retries = MaxRetries;
            bool successful = false;
            while (!successful)
            {
                try
                {

                    int pointsPerDay = invoice.Vehicle.Size < Size.Luxury ? 10 : 15;
                    loyaltyDataService.SubstractPoints(invoice.Customer.Id, pointsPerDay * numberOfDays);

                    invoice.Discount = numberOfDays * invoice.CostPerDay;

                    successful = scope.Complete();

                    #region Logging
                    logger.LogInformation("Redeem complete: {date}", DateTime.Now);
                    #endregion
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
        catch (Exception ex)
        {
            if (!exceptionHandler.Handle(ex))
                throw;
        }
    }
}
