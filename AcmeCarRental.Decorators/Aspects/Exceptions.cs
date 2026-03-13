using AcmeCarRental.Data.Entities;

namespace AcmeCarRental.Decorators.Aspects;

internal class AccureExceptionAspect(ILoyaltyAccrualService service, IExceptionHandler exceptionHandler) : ILoyaltyAccrualService
{
    public void Accrue(RentalAgreement agreement)
    {
        exceptionHandler.Wrapper(() => service.Accrue(agreement));
    }
}

internal class RedeemExceptionAspect(ILoyaltyRedeemService service, IExceptionHandler exceptionHandler) : ILoyaltyRedeemService
{
    public void Redeem(Invoice invoice, int numberOfDays)
    {
        exceptionHandler.Wrapper(() => service.Redeem(invoice, numberOfDays));
    }
}