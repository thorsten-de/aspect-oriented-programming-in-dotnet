using AcmeCarRental.Data.Entities;

using Microsoft.Extensions.Logging.Testing;
using Microsoft.Extensions.Logging;

using static AcmeCarRental.FixtureBuilders;

namespace AcmeCarRental;

public class LoyaltyAccrualServiceTest
{
    private readonly FakeLogger _fakeLogger = new();
    private readonly FlakyDataService _dataService = new();
    private readonly LoyaltyAccrualService _service = null!;
    private readonly FakeTransactionManager _transactions = new();

    public LoyaltyAccrualServiceTest()
    {
        _service = new LoyaltyAccrualService(_dataService, _fakeLogger, _transactions);
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
        Assert.Equal(expectedPointsEarned, _dataService[rentalAgreement.Customer.Id]);
        Assert.Equal(4, _fakeLogger.Collector.Count);
        Assert.Equal(LogLevel.Information, _fakeLogger.LatestRecord.Level);

        Assert.Equal(TransactionState.Commit, _transactions.LastScopeState);
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
