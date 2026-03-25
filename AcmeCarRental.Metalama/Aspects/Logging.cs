using AcmeCarRental.Data.Entities;
using Metalama.Extensions.DependencyInjection;
using Metalama.Framework.Aspects;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace AcmeCarRental.Metalama.Aspects;

public class LoggingAttribute : OverrideMethodAspect
{
    /// <summary>
    /// Gets the logger instance to be used for logging. This is a dependency injected by the framework somehow
    /// </summary>
    [IntroduceDependency]
    private readonly ILogger _logger = null!;


    public override dynamic? OverrideMethod()
    {
        _logger.LogInformation("{methodName}: {date}", meta.Target.Method.Name, DateTime.Now);

        if (meta.Target.Type.Name == "LoyaltyAccrualService")
        {
            RentalAgreement data = meta.RunTime<RentalAgreement>(meta.Target.Parameters[0].Value);
            _logger.LogInformation("Customer: {customerId}", data.Customer?.Id );
            _logger.LogInformation("Vehicle: {vehicleId}", data.Vehicle?.Id);
        }
        else if (meta.Target.Type.Name == "LoyaltyRedeemService")
        {
             Invoice data = meta.RunTime<Invoice>(meta.Target.Parameters[0].Value);
            _logger.LogInformation("Invoice: {invoiceId}", data.Id) ;
        }
        
       
        var result = meta.Proceed();

        _logger.LogInformation("{methodName} completed: {date}", meta.Target.Method.Name, DateTime.Now);
        
        return result;
    }
}
