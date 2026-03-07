namespace AcmeCarRental.NoAOP.Tests;

public class LoyaltyAccrualServiceNoAOPTest : LoyaltyAccrualServiceTest
{
    protected override ILoyaltyAccrualService CreateService()
    {
        return new LoyaltyAccrualService(_dataService, _fakeLogger, _transactions, _exceptionHandlerMock.Object);
    }
}
