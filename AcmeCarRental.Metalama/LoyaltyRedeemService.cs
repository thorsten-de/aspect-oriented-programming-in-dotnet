using AcmeCarRental.Data;
using AcmeCarRental.Data.Entities;
using AcmeCarRental.Metalama.Aspects;

using Metalama.Patterns.Contracts;

namespace AcmeCarRental.Metalama;

internal class LoyaltyRedeemService(ILoyaltyDataService loyaltyDataService) : ILoyaltyRedeemService
{
    [Logging]
    public void Redeem([Required] Invoice invoice, [StrictlyPositive] int numberOfDays)
    {
        int pointsPerDay = invoice.Vehicle.Size < Size.Luxury ? 10 : 15;
        loyaltyDataService.SubstractPoints(invoice.Customer.Id, pointsPerDay * numberOfDays);

        invoice.Discount = numberOfDays * invoice.CostPerDay;
    }
}
