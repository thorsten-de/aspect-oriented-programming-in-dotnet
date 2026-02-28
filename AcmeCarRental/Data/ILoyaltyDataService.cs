namespace AcmeCarRental.Data;

/// <summary>
/// Defines the contract for a service that manages loyalty data for customers.
/// </summary>
public interface ILoyaltyDataService
{
    /// <summary>
    /// Adds the specified number of loyalty points to a customer.
    /// </summary>
    /// <remarks>
    /// This method updates the customer's loyalty points balance. Ensure that the specified customer
    /// exists before calling this method.
    /// </remarks>
    /// <param name="customerId">The unique identifier of the customer whose loyalty points balance will be updated.</param>
    /// <param name="points">The number of points to add to the customer's account. Must be a positive integer.</param>
    void AddPoints(Guid customerId, int points);

    /// <summary>
    /// Subtracts the specified number of loyalty points from the balance of the customer
    /// </summary>
    /// <remarks>
    /// Ensure that the customer has a sufficient points balance before calling this method to
    /// prevent negative balances.
    /// </remarks>
    /// <param name="customerId">The unique identifier of the customer whose points will be subtracted.</param>
    /// <param name="points">The number of points to subtract from the customer's account. Must be a positive integer.</param>
    void SubstractPoints(Guid customerId, int points);
}
