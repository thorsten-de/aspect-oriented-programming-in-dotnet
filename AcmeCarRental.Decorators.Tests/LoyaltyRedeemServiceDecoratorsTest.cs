using AcmeCarRental.Decorators.Aspects;

namespace AcmeCarRental.Decorators.Tests;

public class LoyaltyRedeemServiceDecoratorsTest : LoyaltyRedeemServiceTest
{
    protected override ILoyaltyRedeemService CreateService()
    {
        return new RedeemExceptionAspect(
            new RedeemTransactionAspect(
                 new LoyaltyRedeemService(_dataService, _fakeLogger),
                _transactions),
            _exceptionHandlerMock.Object);
    }
}
