namespace AcmeCarRental.Refactored.Tests;

public class LoyaltyAccrualServiceRefactoredTest : LoyaltyAccrualServiceTest
{
    protected override ILoyaltyAccrualService CreateService()
    {
        return new LoyaltyAccrualService(_dataService, _fakeLogger,
            new TransactionFacade(_transactions, _exceptionHandlerMock.Object));
    }
}
