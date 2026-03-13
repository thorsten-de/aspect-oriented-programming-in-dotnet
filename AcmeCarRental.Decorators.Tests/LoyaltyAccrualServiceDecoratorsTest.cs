using AcmeCarRental.Decorators.Aspects;

namespace AcmeCarRental.Decorators.Tests;

public class LoyaltyAccrualServiceDecoratorsTest : LoyaltyAccrualServiceTest
{
    protected override ILoyaltyAccrualService CreateService()
    {
        return new AccureExceptionAspect(
            new AccrualTransactionAspect(
                new LoyaltyAccrualService(_dataService, _fakeLogger),
                _transactions),
            _exceptionHandlerMock.Object);

    }
}
