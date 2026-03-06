using AcmeCarRental.Data.Entities;

namespace AcmeCarRental;

/// <summary>
/// Create instances of entities with default values for testing purposes. Uses Object Mother pattern to allow overriding default values when needed.
/// </summary>
internal static class FixtureBuilders
{
    public static Customer Customer(string name = "John Doe", string driversLicence = "D1234567", DateTime? dayOfBirth = null)
    {
        return new Customer
        {
            Name = name,
            DriversLicence = driversLicence,
            DayOfBirth = dayOfBirth ?? new DateTime(1982, 2, 17)
        };
    }

    public static Vehicle Vehicle(string make = "VW", string model = "Golf", string vin = "1HGBH41JXMN109186", Size size = Size.Compact)
    {
        return new Vehicle
        {
            Make = make,
            Model = model,
            Vin = vin,
            Size = size
        };
    }
}
