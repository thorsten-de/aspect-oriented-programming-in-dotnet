using Metalama.Extensions.DependencyInjection.ServiceLocator;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Testing;

namespace AcmeCarRental.Metalama.Tests;

public class ServiceLocatorFixture
{
    private readonly FakeLogger _logger = new();

    public ServiceLocatorFixture()
    {
        var serviceCollection = new ServiceCollection()
            .AddSingleton<ILogger>(_logger);

        var serviceProvider = serviceCollection.BuildServiceProvider();

        ServiceProviderProvider.ServiceProvider = () => serviceProvider;
    }

    public FakeLogger GetCleanLogger()
    {
        _logger.Collector.Clear();
        return _logger;
    }
}
