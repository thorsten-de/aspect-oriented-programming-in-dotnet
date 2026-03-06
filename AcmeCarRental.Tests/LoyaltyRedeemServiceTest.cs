using Moq;

using AcmeCarRental.Data;
using AcmeCarRental.Data.Entities;

using static AcmeCarRental.FixtureBuilders;
using Microsoft.Extensions.Logging.Testing;
using Microsoft.Extensions.Logging;

namespace AcmeCarRental;

public class LoyaltyRedeemServiceTest
{
    private readonly FlakyDataService _dataService = new();
    private readonly FakeLogger _fakeLogger = new();
    private readonly LoyaltyRedeemService _service = null!;
    private readonly FakeTransactionManager _transactions = new();

    public LoyaltyRedeemServiceTest()
    {
        _service = new LoyaltyRedeemService(_dataService, _fakeLogger, _transactions);
    }

    [Theory]
    [InlineData(Size.FullSize, 30)]
    [InlineData(Size.Luxury, 45)]
    [InlineData(Size.Truck, 45)]
    public void Redeem_ShouldSubstractLoyaltyPoints_WhenInvoiceIsValid(Size vehicleSize, int expectedPointsRedeemed)
    {
        // Arrange
        var invoice = new Invoice
        {
            Customer = Customer(),
            Vehicle = Vehicle(size: vehicleSize),
            CostPerDay = 29.95m,
        };

        // Act
        _service.Redeem(invoice, numberOfDays: 3);

        // Assert
        Assert.Equal(89.85m, invoice.Discount);
        Assert.Equal(-expectedPointsRedeemed, _dataService[invoice.Customer.Id]);
        Assert.Equal(3, _fakeLogger.Collector.Count);
        Assert.Equal(LogLevel.Information, _fakeLogger.LatestRecord.Level);

        Assert.Equal(TransactionState.Commit, _transactions.LastScopeState);
    }

    [Fact]
    public void Redeem_ShouldThrowException_WhenInvoiceIsNull()
    {
        Assert.Throws<ArgumentNullException>("invoice", () =>
        {
            _service.Redeem(null!, numberOfDays: 3);
        });
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public void Redeem_ShouldThrowException_WhenNumberOfDaysNotPositive(int invalidDays)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>("numberOfDays", () =>
        {
            var invoice = new Invoice
            {
                Customer = Customer(),
                Vehicle = Vehicle(),
                CostPerDay = 10m
            };
            _service.Redeem(invoice, numberOfDays: invalidDays);
        });
    }

    [Fact]
    public void Redeem_ShouldSpendLoyaltyPoints_WhenRetriedSuccessfully()
    {
        _dataService.SimulateFailingAttempts = 3;

        // Arrange
        var invoice = new Invoice
        {
            Customer = Customer(),
            Vehicle = Vehicle(),
            CostPerDay = 29.95m,
        };

        // Act
        _service.Redeem(invoice, numberOfDays: 3);

        // Assert
        Assert.True(_dataService[invoice.Customer.Id] < 0, "Expected that the customer has redeemed points.");
    }

    [Fact]
    public void Redeem_ShouldThrowException_WhenRetriesFail()
    {
        _dataService.SimulateFailingAttempts = 4;

        // Arrange
        var invoice = new Invoice
        {
            Customer = Customer(),
            Vehicle = Vehicle(),
            CostPerDay = 29.95m,
        };

        Assert.ThrowsAny<Exception>(() => _service.Redeem(invoice, numberOfDays: 3));
    }
}
