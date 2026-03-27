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
                        new RedeemTransactionAspect(
                            new LoyaltyRedeemService(_dataService),
                            _transactions),
                        _exceptionHandlerMock.Object);
    }

    private void SetupServiceLocator()
    {
        var serviceCollection = new ServiceCollection()
        .AddSingleton<ILogger>(_fakeLogger);
        var serviceProvider = serviceCollection.BuildServiceProvider();

        ServiceProviderProvider.ServiceProvider = () => serviceProvider;
    }
}