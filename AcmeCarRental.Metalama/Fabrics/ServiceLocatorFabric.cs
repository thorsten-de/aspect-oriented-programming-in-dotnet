using Metalama.Extensions.DependencyInjection;
using Metalama.Extensions.DependencyInjection.ServiceLocator;
using Metalama.Framework.Fabrics;

namespace AcmeCarRental.Metalama.Fabrics;

/// <summary>
/// Provides a project fabric that configures dependency injection using the Service Locator pattern.
/// We use this because we don't have a dependency injection framework in place, and this allows us 
/// to use the Metalama aspects that require dependency injection without having to set up a full DI framework.
/// </summary>
/// <remarks>
/// This class enables projects to use a service locator-based dependency injection framework by
/// registering the appropriate implementation during project amendment. 
/// </remarks>
internal class ServiceLocatorFabric : ProjectFabric
{
    public override void AmendProject(IProjectAmender amender)
    {
        amender.ConfigureDependencyInjection(options => 
            options.RegisterFramework<ServiceLocatorDependencyInjectionFramework>());
    }
}
