using Moq;

using AcmeCarRental.Data;
using AcmeCarRental.Data.Entities;

using static AcmeCarRental.FixtureBuilders;

namespace AcmeCarRental;

public class LoyaltyAccrualServiceTest
{
    [Theory]
    [InlineData(Size.Compact, 3)]
    [InlineData(Size.Luxury, 6)]
    [InlineData(Size.SUV, 6)]
    public void Accrue_ShouldAddLoyaltyPoints_WhenRentalAgreementIsValid(Size vehicleSize, int expectedPointsEarned)
    {
        // Arrange
        var dataService = new Mock<ILoyaltyDataService>();
        var service = new LoyaltyAccrualService(dataService.Object);
        var rentalAgreement = new RentalAgreement
        {
            Customer = Customer(),
            Vehicle = Vehicle(size: vehicleSize),
            StartDate = new(2026, 01, 03),
            EndDate = new(2026, 01, 06)
        };

        // Act
        service.Accrue(rentalAgreement);

        // Assert
        dataService.Verify(service => service.AddPoints(rentalAgreement.Customer.Id, expectedPointsEarned), Times.Once);
    }
}
