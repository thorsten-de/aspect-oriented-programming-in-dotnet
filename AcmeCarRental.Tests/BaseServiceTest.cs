using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Testing;
using Moq;

namespace AcmeCarRental;

public abstract class BaseServiceTest
{
    protected readonly FlakyDataService _dataService = new();
    protected readonly Mock<IExceptionHandlerWithWrapper> _exceptionHandlerMock = new() { CallBase = true };
    protected readonly FakeTransactionManager _transactions = new();
    protected FakeLogger _fakeLogger = new();
}