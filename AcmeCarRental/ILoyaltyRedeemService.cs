using AcmeCarRental.Data.Entities;

namespace AcmeCarRental;

/// <summary>
/// The contract for redeeming loyalty points for customers
/// </summary>
public interface ILoyaltyRedeemService
{
    /// <summary>
    /// Redeems loyalty points for a given invoice and number of days.
    /// </summary>
    /// <param name="invoice">The invoice that is discounted with loyalty points.</param> 
    /// <param name="numberOfDays">The number of days that are discounted by redeeming loyalty points.</param>
    void Redeem(Invoice invoice, int numberOfDays);
}
