namespace AcmeCarRental.Decorators.Tests;

public class LoyaltyRedeemServiceDecoratorsTest : LoyaltyRedeemServiceTest
{
    protected override ILoyaltyRedeemService CreateService()
    {
        return new LoyaltyRedeemService(_dataService, _fakeLogger,
            new TransactionFacade(_transactions, _exceptionHandlerMock.Object));
    }
}
