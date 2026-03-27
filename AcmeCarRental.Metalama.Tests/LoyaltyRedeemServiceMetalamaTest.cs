using AcmeCarRental.Metalama.Aspects;
using Metalama.Extensions.DependencyInjection.ServiceLocator;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AcmeCarRental.Metalama.Tests;

public class LoyaltyRedeemServiceMetalamaTest : LoyaltyRedeemServiceTest
{

    protected override ILoyaltyRedeemService CreateService()
    {
        SetupServiceLocator();

        return
                    new RedeemExceptionAspect(
                            new LoyaltyRedeemService(_dataService),
                        _exceptionHandlerMock.Object);
    }

    private void SetupServiceLocator()
    {
        var serviceProvider = new ServiceCollection()
            .AddSingleton<ILogger>(_fakeLogger)
            .AddSingleton<ITransactionManager>(_transactions)
            .BuildServiceProvider();

        ServiceProviderProvider.ServiceProvider = () => serviceProvider;
    }
}