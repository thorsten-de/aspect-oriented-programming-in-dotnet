namespace AcmeCarRental.Data.Entities;

public class RentalAgreement
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required Customer Customer { get; init; }
    public required Vehicle Vehicle { get; init; }
    public required DateTime StartDate { get; init; }
    public required DateTime EndDate { get; init; }
}