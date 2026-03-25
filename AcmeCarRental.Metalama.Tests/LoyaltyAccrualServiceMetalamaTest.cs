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
        new AccureAssertPreconditions(
                new AccureExceptionAspect(
                    new AccrualTransactionAspect(
                        new LoyaltyAccrualService(_dataService),
                        _transactions),
                    _exceptionHandlerMock.Object));
    }

    private void SetupServiceLocator()
    {
        var serviceCollection = new ServiceCollection()
        .AddSingleton<ILogger>(_fakeLogger);
        var serviceProvider = serviceCollection.BuildServiceProvider();

        ServiceProviderProvider.ServiceProvider = () => serviceProvider;
    }
}
