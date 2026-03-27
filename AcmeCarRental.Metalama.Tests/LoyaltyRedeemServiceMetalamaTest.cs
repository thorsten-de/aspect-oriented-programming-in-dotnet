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

        return new LoyaltyRedeemService(_dataService);
    }

    private void SetupServiceLocator()
    {
        var serviceProvider = new ServiceCollection()
            .AddSingleton<ILogger>(_fakeLogger)
            .AddSingleton<ITransactionManager>(_transactions)
            .AddSingleton<IExceptionHandler>(_exceptionHandlerMock.Object)
            .BuildServiceProvider();

        ServiceProviderProvider.ServiceProvider = () => serviceProvider;
    }
}