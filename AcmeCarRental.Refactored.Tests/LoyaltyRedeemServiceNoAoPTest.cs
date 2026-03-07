namespace AcmeCarRental.Refactored.Tests;

public class LoyaltyRedeemServiceRefactoredTest : LoyaltyRedeemServiceTest
{
    protected override ILoyaltyRedeemService CreateService()
    {
        return new LoyaltyRedeemService(_dataService, _fakeLogger, _transactions, _exceptionHandlerMock.Object);
    }
}
