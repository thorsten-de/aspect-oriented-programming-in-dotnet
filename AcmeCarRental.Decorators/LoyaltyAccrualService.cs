using AcmeCarRental.Data;
using AcmeCarRental.Data.Entities;

namespace AcmeCarRental.Decorators;

internal class LoyaltyAccrualService(ILoyaltyDataService loyaltyDataService) : ILoyaltyAccrualService
{
    public void Accrue(RentalAgreement agreement)
    {
        #region Defensive programming
        ArgumentNullException.ThrowIfNull(agreement);
        #endregion

        var rentalTimeSpan = agreement.EndDate - agreement.StartDate;
        int numberOfDays = (int)Math.Floor(rentalTimeSpan.TotalDays);
        int pointsPerDay = agreement.Vehicle.Size < Size.Luxury ? 1 : 2;

        loyaltyDataService.AddPoints(agreement.Customer.Id, numberOfDays * pointsPerDay);
    }
}
