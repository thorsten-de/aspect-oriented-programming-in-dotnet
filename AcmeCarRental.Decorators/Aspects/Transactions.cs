using AcmeCarRental.Data.Entities;

namespace AcmeCarRental.Decorators.Aspects;

internal class AccrualTransactionAspect(ILoyaltyAccrualService service, ITransactionManager transactionManager) : ILoyaltyAccrualService
{
    public void Accrue(RentalAgreement agreement)
    {
        transactionManager.Wrapper(() => service.Accrue(agreement));
    }
}

internal class RedeemTransactionAspect(ILoyaltyRedeemService service, ITransactionManager transactionManager) : ILoyaltyRedeemService
{
    public void Redeem(Invoice invoice, int numberOfDays)
    {
        transactionManager.Wrapper(() => service.Redeem(invoice, numberOfDays));
    }
}
