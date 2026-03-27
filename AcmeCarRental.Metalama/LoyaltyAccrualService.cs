using AcmeCarRental.Data;
using AcmeCarRental.Data.Entities;
using AcmeCarRental.Metalama.Aspects;
using Metalama.Patterns.Contracts;

namespace AcmeCarRental.Metalama;

internal class LoyaltyAccrualService(ILoyaltyDataService loyaltyDataService) : ILoyaltyAccrualService
{
    [Logging]
    [TransactionManagement]
    [ExceptionHandling]
    public void Accrue([Required] RentalAgreement agreement)
    {
        var rentalTimeSpan = agreement.EndDate - agreement.StartDate;
        int numberOfDays = (int)Math.Floor(rentalTimeSpan.TotalDays);
        int pointsPerDay = agreement.Vehicle.Size < Size.Luxury ? 1 : 2;

        loyaltyDataService.AddPoints(agreement.Customer.Id, numberOfDays * pointsPerDay);
    }
}
