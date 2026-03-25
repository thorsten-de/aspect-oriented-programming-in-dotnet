using AcmeCarRental.Data.Entities;

using Microsoft.Extensions.Logging;

using static AcmeCarRental.FixtureBuilders;
using Moq;

namespace AcmeCarRental;

public abstract class LoyaltyAccrualServiceTest : BaseServiceTest
{
    protected readonly ILoyaltyAccrualService _service = null!;

    public LoyaltyAccrualServiceTest()
    {
        _service = CreateService();
    }

    protected abstract ILoyaltyAccrualService CreateService();

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

    [Fact]
    public void Accrue_ShouldAddLoyaltyPoints_WhenRetriedSuccessfully()
    {
        // Arrange
        _dataService.SimulateFailingAttempts = 3;
        var rentalAgreement = new RentalAgreement
        {
            Customer = Customer(),
            Vehicle = Vehicle(),
            StartDate = new(2026, 01, 03),
            EndDate = new(2026, 01, 06)
        };

        // Act
        _service.Accrue(rentalAgreement);

        // Assert
        Assert.True(_dataService[rentalAgreement.Customer.Id] > 0, "Expected that the customer has accrued points.");
    }

    [Fact]
    public void Accrue_ShouldThrowException_WhenRetriesFail()
    {
        // Arrange
        _dataService.SimulateFailingAttempts = 4;
        var rentalAgreement = new RentalAgreement
        {
            Customer = Customer(),
            Vehicle = Vehicle(),
            StartDate = new(2026, 01, 03),
            EndDate = new(2026, 01, 06)
        };

        Assert.ThrowsAny<Exception>(() => _service.Accrue(rentalAgreement));
        _exceptionHandlerMock.Verify(h => h.Handle(It.IsAny<TimeoutException>()), Times.Once);
    }

    [Fact]
    public void Accrue_ShouldHandleException_WhenExceptionHandlerHandlesException()
    {
        // Arrange
        _dataService.SimulateFailingAttempts = 4;
        _exceptionHandlerMock.Setup(h => h.Handle(It.IsAny<TimeoutException>())).Returns(true);

        var rentalAgreement = new RentalAgreement
        {
            Customer = Customer(),
            Vehicle = Vehicle(),
            StartDate = new(2026, 01, 03),
            EndDate = new(2026, 01, 06)
        };

        _service.Accrue(rentalAgreement);

        // Assert
        Assert.Equal(0, _dataService[rentalAgreement.Customer.Id]);
        _exceptionHandlerMock.Verify(h => h.Handle(It.IsAny<TimeoutException>()), Times.Once);
    }
}
