using AcmeCarRental.Metalama.Aspects;

namespace AcmeCarRental.Metalama.Tests;

public class LoyaltyRedeemServiceMetalamaTest : LoyaltyRedeemServiceTest
{
    protected override ILoyaltyRedeemService CreateService()
    {
        return
            new RedeemAssertPreconditions(
                new RedeemLoggingAspect(
                    new RedeemExceptionAspect(
                        new RedeemTransactionAspect(
                            new LoyaltyRedeemService(_dataService),
                            _transactions),
                        _exceptionHandlerMock.Object),
                    _fakeLogger));
    }
}