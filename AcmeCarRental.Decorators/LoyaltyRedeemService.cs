using AcmeCarRental.Data;
using AcmeCarRental.Data.Entities;
using Microsoft.Extensions.Logging;

namespace AcmeCarRental.Decorators;

internal class LoyaltyRedeemService(ILoyaltyDataService loyaltyDataService) : ILoyaltyRedeemService
{
    public void Redeem(Invoice invoice, int numberOfDays)
    {
        #region Defensive programming
        ArgumentNullException.ThrowIfNull(invoice);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(numberOfDays);
        #endregion

        int pointsPerDay = invoice.Vehicle.Size < Size.Luxury ? 10 : 15;
        loyaltyDataService.SubstractPoints(invoice.Customer.Id, pointsPerDay * numberOfDays);

        invoice.Discount = numberOfDays * invoice.CostPerDay;
    }
}
