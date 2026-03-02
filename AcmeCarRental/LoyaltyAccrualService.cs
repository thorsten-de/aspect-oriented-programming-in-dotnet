using AcmeCarRental.Data;
using AcmeCarRental.Data.Entities;
using Microsoft.Extensions.Logging;

namespace AcmeCarRental;

internal class LoyaltyAccrualService(ILoyaltyDataService loyaltyDataService, ILogger logger) : ILoyaltyAccrualService
{
    public void Accrue(RentalAgreement agreement)
    {
        #region Logging
        logger.LogInformation("Accrue: {date}", DateTime.Now);
        logger.LogInformation("Customer: {customerId}", agreement.Customer.Id);
        logger.LogInformation("Vehicle: {vehicleId}", agreement.Vehicle.Id);
        #endregion


        var rentalTimeSpan = agreement.EndDate - agreement.StartDate;
        int numberOfDays = (int)Math.Floor(rentalTimeSpan.TotalDays);
        int pointsPerDay = agreement.Vehicle.Size < Size.Luxury ? 1 : 2;

        loyaltyDataService.AddPoints(agreement.Customer.Id, numberOfDays * pointsPerDay);

        #region Logging
        logger.LogInformation("Accrue complete: {date}", DateTime.Now);
        #endregion
    }
}
