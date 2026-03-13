using AcmeCarRental.Data;
using AcmeCarRental.Data.Entities;
using Microsoft.Extensions.Logging;

namespace AcmeCarRental.Decorators;

internal class LoyaltyRedeemService(
    ILoyaltyDataService loyaltyDataService,
    ILogger logger
    ) : ILoyaltyRedeemService
{
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

        int pointsPerDay = invoice.Vehicle.Size < Size.Luxury ? 10 : 15;
        loyaltyDataService.SubstractPoints(invoice.Customer.Id, pointsPerDay * numberOfDays);

        invoice.Discount = numberOfDays * invoice.CostPerDay;

        #region Logging
        logger.LogInformation("Redeem complete: {date}", DateTime.Now);
        #endregion
    }
}
