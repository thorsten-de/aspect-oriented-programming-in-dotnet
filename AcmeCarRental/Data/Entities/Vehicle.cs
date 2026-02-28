namespace AcmeCarRental.Data.Entities;

/// <summary>
/// A vehicle available for rent in the Acme Car Rental system.
/// </summary>
public class Vehicle
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Make { get; init; }
    public required string Model { get; init; }
    
    /// <summary>
    /// The size category of the vehicle, which is considered when earning and spending loalty points.
    /// </summary>
    public required Size Size { get; init; }

    /// <summary>
    /// Gets the vehicle identification number (VIN) of the vehicle.
    /// </summary>
    public required string Vin { get; init; }
}

/// <summary>
/// Specifies the size category of a vehicle.
/// </summary>
public enum Size
{
    Compact,
    MidSize,
    FullSize,
    Luxury,
    Truck,
    SUV
}