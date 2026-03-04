namespace AcmeCarRental;

public interface ITransactionScope : IDisposable
{
    void Complete();
}
