using System;
using AcmeCarRental.Data;

namespace AcmeCarRental;

/// <summary>
/// A test implementation of ILoyaltyDataService that simulates a flaky data store. It allows you to specifya
/// a number of failing attempts before the operations succeed, which is useful for testing the retry logic 
/// in the services. The data can be accessed using an indexer to get the current loyalty points for a given customer.
/// </summary>
internal class FlakyDataService : ILoyaltyDataService
{
    private readonly Dictionary<Guid, int> _data = [];

    /// <summary>
    /// Gets the current loyalty points for a given customer. If the customer does not exist in the data store, it returns 0.
    /// </summary>
    /// <param name="customerId"></param>
    /// <returns>Customer's loyalty points, or 0 if the customer does not exist.</returns>
    public int this[Guid customerId] => _data.GetValueOrDefault(customerId, 0);

    /// <summary>
    /// Number of attempts to simulate a failure before the operation succeeds. This is used to test the retry logic in the services.
    /// </summary>
    public int SimulateFailingAttempts { get; set; } = 0;

    /// <summary>
    /// Adds points to the customer's loyalty account. This method simulates a failure by throwing a TimeoutException for a specified number of attempts before succeeding.
    /// </summary>
    /// <param name="customerId"></param>
    /// <param name="points"></param>
    /// <exception cref="TimeoutException"></exception>
    public void AddPoints(Guid customerId, int points)
    {
        if (SimulateFailingAttempts-- > 0)
            throw new TimeoutException("This simulates a timeout");

        _data[customerId] = this[customerId] + points;
    }

    /// <summary>
    /// Subtracts points from the customer's loyalty account. This method simulates a failure by throwing a TimeoutException for a specified number of attempts before succeeding.
    /// </summary>
    /// <param name="customerId"></param>
    /// <param name="points"></param>
    /// <exception cref="TimeoutException"></exception>
    public void SubstractPoints(Guid customerId, int points)
    {
        if (SimulateFailingAttempts-- > 0)
            throw new TimeoutException("This simulates a timeout");

        _data[customerId] = this[customerId] - points;
    }
}
