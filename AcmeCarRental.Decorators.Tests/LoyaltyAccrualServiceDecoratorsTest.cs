using AcmeCarRental.Decorators.Aspects;

namespace AcmeCarRental.Decorators.Tests;

public class LoyaltyAccrualServiceDecoratorsTest : LoyaltyAccrualServiceTest
{
    protected override ILoyaltyAccrualService CreateService()
    {
        return
            new AccrueLoggingAspect(
                new AccureExceptionAspect(
                    new AccrualTransactionAspect(
                        new LoyaltyAccrualService(_dataService),
                        _transactions),
                    _exceptionHandlerMock.Object),
                _fakeLogger);
    }
}
