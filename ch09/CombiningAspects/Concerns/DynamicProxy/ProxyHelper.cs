using Castle.DynamicProxy;

namespace CombiningAspects.Concerns.DynamicProxy;

/// <summary>
/// Helper class to create proxies with the specified interceptor.
/// </summary>
public class ProxyHelper()
{
    /// <summary>
    /// The Castle DynamicProxy generator instance used to create proxies.
    /// </summary>
    private readonly ProxyGenerator _generator = new();


    /// <summary>
    /// Creates a proxy for the given object using the specified interceptor. The interceptor is resolved
    /// from the ObjectFactory.
    /// </summary>
    /// <typeparam name="T">The type of the interface to proxy.</typeparam>
    /// <typeparam name="K">The Interceptor type to use for the proxy.</typeparam>
    /// <param name="obj">The actual object to proxy.</param>
    /// <returns>A proxied instance of the specified type.</returns>
    public T Proxify<T, K>(object obj) where K : IInterceptor =>
        (T)Proxify<K>(typeof(T), obj);

    /// <summary>
    /// Creates a proxy for the given object using the specified interceptor. The interceptor is resolved 
    /// from the ObjectFactory
    /// </summary>
    /// <typeparam name="K">The Interceptor type to use for the proxy.</typeparam>
    /// <param name="t">The type of the interface to proxy.</param>
    /// <param name="obj">The actual object to proxy.</param>
    /// <returns>A proxied instance of the specified type.</returns>
    public object Proxify<K>(Type t, object obj) where K : IInterceptor
    {
        var interceptor = ObjectFactory.GetInstance<K>();
        var result = _generator.CreateInterfaceProxyWithTargetInterface(t, obj, interceptor);
        return result;
    }

    /// <summary>
    /// Creates a proxy for the given object using the specified named interceptor. The interceptor is resolved
    /// from the ObjectFactory using the provided name.
    /// </summary>
    /// <typeparam name="T">The type of the interface to proxy.</param>
    /// <typeparam name="K">The Interceptor type to use for the proxy.</typeparam>
    /// <param name="obj">The actual object to proxy.</param>
    /// <returns>A proxied instance of the specified type.</returns>
    /// <returns></returns>
    public T Proxify<T, K>(string name, object obj) where K : IInterceptor
    {
        var interceptor = ObjectFactory.GetNamedInstance<K>(name);
        var result = _generator.CreateInterfaceProxyWithTargetInterface(typeof(T), obj, interceptor);
        return (T)result;
    }
}

