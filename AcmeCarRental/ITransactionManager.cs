namespace AcmeCarRental;

public interface ITransactionManager
{
    ITransactionScope CreateScope();
}

