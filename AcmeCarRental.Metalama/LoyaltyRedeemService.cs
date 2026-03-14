using AcmeCarRental.Data;
using AcmeCarRental.Data.Entities;

namespace AcmeCarRental.Metalama;

internal class LoyaltyRedeemService(ILoyaltyDataService loyaltyDataService) : ILoyaltyRedeemService
{
    public void Redeem(Invoice invoice, int numberOfDays)
    {
        int pointsPerDay = invoice.Vehicle.Size < Size.Luxury ? 10 : 15;
        loyaltyDataService.SubstractPoints(invoice.Customer.Id, pointsPerDay * numberOfDays);

        invoice.Discount = numberOfDays * invoice.CostPerDay;
    }
}
