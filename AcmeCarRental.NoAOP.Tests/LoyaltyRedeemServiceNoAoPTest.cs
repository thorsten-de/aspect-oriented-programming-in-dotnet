namespace AcmeCarRental.NoAOP.Tests;

public class LoyaltyRedeemServiceNoAOPTest : LoyaltyRedeemServiceTest
{
    protected override ILoyaltyRedeemService CreateService()
    {
        return new LoyaltyRedeemService(_dataService, _fakeLogger, _transactions, _exceptionHandlerMock.Object);
    }
}
