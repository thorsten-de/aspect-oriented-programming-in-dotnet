using AcmeCarRental.Data.Entities;
using Microsoft.Extensions.Logging;

namespace AcmeCarRental.Metalama.Aspects;

/// <summary>
/// This aspect is responsible for logging the execution of the loyalty accrual and redeem services. It logs the start 
/// and end of the execution, as well as the relevant data (customer, vehicle, invoice) for each operation.
/// </summary>
/// <param name="service">The service to decorate with logging.</param>
/// <param name="logger">The logger to use for logging.</param>
internal class AccrueLoggingAspect(ILoyaltyAccrualService service, ILogger logger) : ILoyaltyAccrualService
{
    public void Accrue(RentalAgreement agreement)
    {
        logger.LogInformation("Accrue: {date}", DateTime.Now);
        logger.LogInformation("Customer: {customerId}", agreement?.Customer.Id);
        logger.LogInformation("Vehicle: {vehicleId}", agreement?.Vehicle?.Id);

        service.Accrue(agreement);

        logger.LogInformation("Accrue complete: {date}", DateTime.Now);
    }
}

/// <summary>
/// This aspect is responsible for logging the execution of the loyalty redeem service. It logs the start 
/// and end of the execution, as well as the relevant data (invoice) for each operation.
/// </summary>
/// <param name="service">The service to decorate with logging.</param>
/// <param name="logger">The logger to use for logging.</param>
internal class RedeemLoggingAspect(ILoyaltyRedeemService service, ILogger logger) : ILoyaltyRedeemService
{
    public void Redeem(Invoice invoice, int numberOfDays)
    {
        logger.LogInformation("Redeem: {date}", DateTime.Now);
        logger.LogInformation("Invoice: {invoiceId}", invoice?.Id);

        service.Redeem(invoice, numberOfDays);

        logger.LogInformation("Redeem complete: {date}", DateTime.Now);
    }
}