using AcmeCarRental.Decorators.Aspects;

namespace AcmeCarRental.Decorators.Tests;

public class LoyaltyRedeemServiceDecoratorsTest : LoyaltyRedeemServiceTest
{
    protected override ILoyaltyRedeemService CreateService()
    {
        return
            new RedeemLoggingAspect(
                new RedeemExceptionAspect(
                    new RedeemTransactionAspect(
                        new LoyaltyRedeemService(_dataService),
                        _transactions),
                    _exceptionHandlerMock.Object),
                _fakeLogger);
    }
}