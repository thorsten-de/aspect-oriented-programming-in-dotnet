using CombiningAspects.Concerns.DynamicProxy;
using CombiningAspects.Services;
using Lamar;

/// <summary>
/// A singleton that acts as a service locator, similar to what StructureMap's ObjectFactory used to be. 
/// </summary>
public static class ObjectFactory
{
    private static readonly Container _container;

    static ObjectFactory()
    {
        _container = new Container(x =>
        {
            x.Scan(s =>
            {
                s.TheCallingAssembly();
                s.WithDefaultConventions();
            });

            x.ForConcreteType<AuthorizedInterceptor>()
                .Configure.Ctor<string>("role").Is("Manager")
                .Named("ManagerAuth");

            var proxyHelper = new ProxyHelper();
            x.For<IBudgetService>()
                .InterceptAll(proxyHelper.Proxify<IBudgetService, CachedInterceptor>);
            x.For<IBudgetService>()
                .InterceptAll(w => proxyHelper.Proxify<IBudgetService, AuthorizedInterceptor>("ManagerAuth", w));
        });
    }

    public static T GetInstance<T>() => _container.GetInstance<T>();

    public static T GetNamedInstance<T>(string name) => _container.GetInstance<T>(name);
}

