namespace CombiningAspects.Concerns;

public interface IMethodContextAdapter
{
    object Tag { get; set; }
    object ReturnValue { get; set; }
    string MethodName { get; }
    object[] Arguments { get; }
    void AbortMethod();
}
