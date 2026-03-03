using Moq;

using AcmeCarRental.Data;
using AcmeCarRental.Data.Entities;

using static AcmeCarRental.FixtureBuilders;
using Microsoft.Extensions.Logging.Testing;
using Microsoft.Extensions.Logging;

namespace AcmeCarRental;

public class LoyaltyAccrualServiceTest
{
    private readonly FakeLogger _fakeLogger = new();
    private readonly Mock<ILoyaltyDataService> _dataService = new();
    private readonly LoyaltyAccrualService _service = null!;

    public LoyaltyAccrualServiceTest()
    {
        _service = new LoyaltyAccrualService(_dataService.Object, _fakeLogger);
    }

    [Theory]
    [InlineData(Size.Compact, 3)]
    [InlineData(Size.Luxury, 6)]
    [InlineData(Size.SUV, 6)]
    public void Accrue_ShouldAddLoyaltyPoints_WhenRentalAgreementIsValid(Size vehicleSize, int expectedPointsEarned)
    {
        // Arrange
        var rentalAgreement = new RentalAgreement
        {
            Customer = Customer(),
            Vehicle = Vehicle(size: vehicleSize),
            StartDate = new(2026, 01, 03),
            EndDate = new(2026, 01, 06)
        };

        // Act
        _service.Accrue(rentalAgreement);

        // Assert
        _dataService.Verify(service => service.AddPoints(rentalAgreement.Customer.Id, expectedPointsEarned), Times.Once);
        Assert.Equal(4, _fakeLogger.Collector.Count);
        Assert.Equal(LogLevel.Information, _fakeLogger.LatestRecord.Level);
    }

    [Fact]
    public void Accrue_ShoudThrowException_WhenRentalAgreementIsNull()
    {
        Assert.Throws<ArgumentNullException>("agreement", () =>
        {
            _service.Accrue(null!);
        });
    }
}
