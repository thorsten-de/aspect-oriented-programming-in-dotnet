using Moq;

using AcmeCarRental.Data;
using AcmeCarRental.Data.Entities;

using static AcmeCarRental.FixtureBuilders;
using Microsoft.Extensions.Logging.Testing;
using Microsoft.Extensions.Logging;

namespace AcmeCarRental;

public class LoyaltyRedeemServiceTest
{
    private readonly Mock<ILoyaltyDataService> dataService = new();
    private readonly FakeLogger fakeLogger = new();
    private readonly LoyaltyRedeemService service = null!;

    public LoyaltyRedeemServiceTest()
    {
        service = new LoyaltyRedeemService(dataService.Object, fakeLogger);
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
        service.Redeem(invoice, numberOfDays: 3);

        // Assert
        Assert.Equal(89.85m, invoice.Discount);
        dataService.Verify(service => service.SubstractPoints(invoice.Customer.Id, expectedPointsRedeemed), Times.Once);
        Assert.Equal(3, fakeLogger.Collector.Count);
        Assert.Equal(LogLevel.Information, fakeLogger.LatestRecord.Level);
    }

    [Fact]
    public void Redeem_ShouldThrowException_WhenInvoiceIsNull()
    {
        Assert.Throws<ArgumentNullException>("invoice", () =>
        {
            service.Redeem(null!, numberOfDays: 3);
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
            service.Redeem(invoice, numberOfDays: invalidDays);
        });
    }
}
