using AcmeCarRental.Metalama.Aspects;
using Metalama.Extensions.DependencyInjection.ServiceLocator;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AcmeCarRental.Metalama.Tests;

public class LoyaltyAccrualServiceMetalamaTest : LoyaltyAccrualServiceTest
{
    protected override ILoyaltyAccrualService CreateService()
    {
        SetupServiceLocator();

        return
                new AccureExceptionAspect(
                        new LoyaltyAccrualService(_dataService),
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
