using AcmeCarRental.Data.Entities;

namespace AcmeCarRental.Metalama.Aspects;

/// <summary>
/// This aspect is responsible for asserting preconditions for the ILoyaltyAccrualService and ILoyaltyRedeemService. 
/// It implements the respective service interfaces and decorates an instance of the service. The aspect uses
/// defensive programming techniques to check for null arguments and invalid values before delegating the call 
/// to the decorated service. This allows for precondition checks without cluttering the business logic itself.
/// </summary>
/// <param name="service">The accrual service to decorate.</param>
internal class AccureAssertPreconditions(ILoyaltyAccrualService service) : ILoyaltyAccrualService
{
    public void Accrue(RentalAgreement agreement)
    {
        ArgumentNullException.ThrowIfNull(agreement);

        service.Accrue(agreement);
    }
}

/// <summary>
/// This aspect is responsible for asserting preconditions for the ILoyaltyRedeemService. It implements the
/// ILoyaltyRedeemService interface and decorates an instance of the service. The aspect uses defensive programming 
/// techniques to check for null arguments and invalid values before delegating the call to the decorated service.
/// This allows for precondition checks without cluttering the business logic itself.
/// </summary>
/// <param name="service">The redeem service to decorate.</param>
internal class RedeemAssertPreconditions(ILoyaltyRedeemService service) : ILoyaltyRedeemService
{
    public void Redeem(Invoice invoice, int numberOfDays)
    {
        ArgumentNullException.ThrowIfNull(invoice);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(numberOfDays);

        service.Redeem(invoice, numberOfDays);
    }
}