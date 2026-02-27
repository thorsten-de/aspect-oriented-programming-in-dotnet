using PostSharp.Aspects;
using PostSharp.Serialization;

var myObject = new MyClass();
myObject.MyMethod();


/// <summary>
/// This class contains a method that will be intercepted by the aspect. The aspect will print messages before and after the execution of MyMethod.
/// </summary>
class MyClass
{
    [MyAspect]
    public void MyMethod()
    {
        Console.WriteLine("Hello from MyMethod!");
    }
}

/// <summary>
/// This aspect will print a message before and after the execution of the method it is applied to.
/// </summary>
/// <remarks>
/// If you use the [Serializable] attribute instead of [PSerializable], you will get an error. Using [Serializable] is not supported
/// as it necessitates the use of a BinaryFormatter, which is considered insecure and dangerous. So we use [PSerializable] instead.
/// <seealso cref="https://doc.postsharp.net/deploymentconfiguration/deployment/binary-formatter-security"/>
/// </remarks>
[PSerializable]
public class MyAspect : OnMethodBoundaryAspect
{
    /// <summary>
    /// This method is called before the execution of the method it is applied to. It prints a message to the console.
    /// </summary>
    /// <param name="args">The arguments of the method being intercepted. This parameter is not used in this example, but it can be used to access information about the method being intercepted, such as its name, parameters, etc.</param>
    public override void OnEntry(MethodExecutionArgs args) => Console.WriteLine("Before the method");

    /// <summary>
    /// This method is called after the execution of the method it is applied to. It prints a message to the console.
    /// </summary>
    /// <param name="args">The arguments of the method being intercepted.</param>
    public override void OnExit(MethodExecutionArgs args) => Console.WriteLine("After the method");
}