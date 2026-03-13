namespace AcmeCarRental.Decorators.Tests;

public class LoyaltyAccrualServiceDecoratorsTest : LoyaltyAccrualServiceTest
{
    protected override ILoyaltyAccrualService CreateService()
    {
        return new LoyaltyAccrualService(_dataService, _fakeLogger,
            new TransactionFacade(_transactions, _exceptionHandlerMock.Object));
    }
}
