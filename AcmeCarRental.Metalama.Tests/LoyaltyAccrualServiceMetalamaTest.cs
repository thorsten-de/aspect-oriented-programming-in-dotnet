using AcmeCarRental.Metalama.Aspects;

namespace AcmeCarRental.Metalama.Tests;

public class LoyaltyAccrualServiceMetalamaTest : LoyaltyAccrualServiceTest
{
    protected override ILoyaltyAccrualService CreateService()
    {
        return
        new AccureAssertPreconditions(
            new AccrueLoggingAspect(
                new AccureExceptionAspect(
                    new AccrualTransactionAspect(
                        new LoyaltyAccrualService(_dataService),
                        _transactions),
                    _exceptionHandlerMock.Object),
                _fakeLogger));
    }
}
