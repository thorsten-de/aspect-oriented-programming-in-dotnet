using System;
using AcmeCarRental.Data;

namespace AcmeCarRental;

internal class FlakyDataService : ILoyaltyDataService
{
    private readonly Dictionary<Guid, int> _data = [];

    public int this[Guid customerId] => _data.GetValueOrDefault(customerId, 0);

    public void AddPoints(Guid customerId, int points)
    {
        _data[customerId] = this[customerId] + points;
    }

    public void SubstractPoints(Guid customerId, int points)
    {
        _data[customerId] = this[customerId] - points;
    }
}
