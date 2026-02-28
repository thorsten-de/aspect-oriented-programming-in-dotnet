using AcmeCarRental.Data.Entities;

namespace AcmeCarRental;

/// <summary>
/// Defines a contract for accruing loyalty points for customers based on a rental agreement.
/// </summary>
public interface ILoyaltyAccrualService
{
    /// <summary>
    /// Accrues loyalty points based on the specified rental agreement.
    /// </summary>
    /// <param name="agreement">The rental agreement for which to accrue loyalty points.</param>
    void Accrue(RentalAgreement agreement);
}
