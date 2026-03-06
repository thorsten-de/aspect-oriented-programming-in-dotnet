using AcmeCarRental.Data;
using AcmeCarRental.Data.Entities;
using Microsoft.Extensions.Logging;

namespace AcmeCarRental;

internal class LoyaltyAccrualService(
    ILoyaltyDataService loyaltyDataService,
    ILogger logger,
    ITransactionManager transactions,
    IExceptionHandler exceptionHandler
    ) : ILoyaltyAccrualService
{
    private const int MaxRetries = 3;
    public void Accrue(RentalAgreement agreement)
    {
        #region Defensive programming
        ArgumentNullException.ThrowIfNull(agreement);
        #endregion

        #region Logging
        logger.LogInformation("Accrue: {date}", DateTime.Now);
        logger.LogInformation("Customer: {customerId}", agreement.Customer.Id);
        logger.LogInformation("Vehicle: {vehicleId}", agreement.Vehicle.Id);
        #endregion
        try
        {
            using var scope = transactions.CreateScope();
            int retries = MaxRetries;
            bool succeeded = false;

            while (!succeeded)
            {
                try
                {
                    var rentalTimeSpan = agreement.EndDate - agreement.StartDate;
                    int numberOfDays = (int)Math.Floor(rentalTimeSpan.TotalDays);
                    int pointsPerDay = agreement.Vehicle.Size < Size.Luxury ? 1 : 2;

                    loyaltyDataService.AddPoints(agreement.Customer.Id, numberOfDays * pointsPerDay);

                    succeeded = scope.Complete();

                    #region Logging
                    logger.LogInformation("Accrue complete: {date}", DateTime.Now);
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
