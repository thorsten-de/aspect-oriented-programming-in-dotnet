namespace AcmeCarRental.Data.Entities;

/// <summary>
/// The invoice for a car rental transaction
/// </summary>
public class Invoice
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required Customer Customer { get; init; }
    public required Vehicle Vehicle { get; init; }
    public required decimal CostPerDay { get; init; }
    public decimal Discount { get; set; } = 0m;
}