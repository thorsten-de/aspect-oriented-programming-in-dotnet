using Metalama.Framework.Aspects;

var myObject = new MyClass();
myObject.MyMethod();

class MyClass
{
    /// <summary>
    /// This method is decorated with the <c>MyAspect</c> aspect attribute, 
    /// which will override its execution to add logging.
    /// </summary>
    [MyAspect]
    public void MyMethod() =>
        Console.WriteLine("Hello from MyMethod!");
}

/// <summary>
/// This aspect overrides the method execution to add logging to the console
///  before and after the method call.
/// </summary>
class MyAspectAttribute : OverrideMethodAspect
{
    /// <summary>
    /// This method is called instead of the original method. It logs the entry 
    /// and exit of the method execution.
    /// </summary>
    /// <para>
    /// The <c>meta.Proceed()</c> call executes the original method. These meta 
    /// statements are evaluated at compile time and are replaced with the 
    /// appropriate code to call the original method. The aspect can also modify
    /// the arguments or the return value if needed.
    /// </para>
    /// <para>
    /// The <c>meta.Target.Method</c> expression is evaluated at compile time 
    /// and is replaced with the actual method being overridden.
    /// </para>
    /// <returns>The result of the original method execution, if any.</returns
    public override dynamic? OverrideMethod()
    {
        Console.WriteLine($"Entering {meta.Target.Method}");
        try
        {
            return meta.Proceed();
        }
        finally
        {
            Console.WriteLine($"Leaving {meta.Target.Method}");
        }
    }
}
