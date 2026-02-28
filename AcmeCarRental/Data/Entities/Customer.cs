namespace AcmeCarRental.Data.Entities;

/// <summary>
/// Represents a customer in the Acme Car Rental system.
/// </summary>
public class Customer
{
    /// <summary>
    /// Gets the unique identifier for the customer.
    /// </summary>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>
    /// Gets the customer's full name.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Gets the customer's driver's license number.
    /// </summary>
    public required string DriversLicence { get; init; }

    /// <summary>
    /// Gets the customer's date of birth.
    /// </summary>
    public required DateTime DayOfBirth { get; init; }
}