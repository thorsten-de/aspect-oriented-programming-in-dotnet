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
        });
    }

    public static T GetInstance<T>() => _container.GetInstance<T>();
}

