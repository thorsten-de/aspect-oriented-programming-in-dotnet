using Moq;

using AcmeCarRental.Data;
using AcmeCarRental.Data.Entities;

using static AcmeCarRental.FixtureBuilders;
using Microsoft.Extensions.Logging.Testing;
using Microsoft.Extensions.Logging;

namespace AcmeCarRental;

public class LoyaltyRedeemServiceTest
{
    [Theory]
    [InlineData(Size.FullSize, 30)]
    [InlineData(Size.Luxury, 45)]
    [InlineData(Size.Truck, 45)]
    public void Redeem_ShouldSubstractLoyaltyPoints_WhenInvoiceIsValid(Size vehicleSize, int expectedPointsRedeemed)
    {
        // Arrange
        var dataService = new Mock<ILoyaltyDataService>();
        var fakeLogger = new FakeLogger();
        var service = new LoyaltyRedeemService(dataService.Object, fakeLogger);
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
}
