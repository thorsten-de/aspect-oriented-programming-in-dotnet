using CombiningAspects.Concerns.PostSharp;

namespace CombiningAspects.Services;

/// <summary>
/// Defines the contract for a budget service, which provides budget information for accounts.
/// </summary>
public interface IBudgetService
{
    /// <summary>
    /// Retrieves the budget for a given account number.
    /// </summary>
    /// <param name="accountNumber">The account number to retrieve the budget for.</param>
    /// <returns>The budget amount for the specified account.</returns>
    decimal GetBudgetForAccount(string accountNumber);
}

/// <summary>
/// Implements the IBudgetService interface to provide budget information for accounts.
/// The GetBudgetForAccount method is decorated with both the Cached and Authorized attributes,
/// which means it will have caching and authorization concerns applied to it.
/// </summary>
public class BudgetService : IBudgetService
{
    [Cached]
    [Authorized("Manager")]
    public decimal GetBudgetForAccount(string accountNumber) =>
        Random.Shared.Next(1000, 5000);
}
