using CombiningAspects.Concerns.PostSharp;

namespace CombiningAspects.Services;

public interface IBudgetService
{
    decimal GetBudgetForAccount(string accountNumber);
}

public class BudgetService : IBudgetService
{
    [Cached]
    public decimal GetBudgetForAccount(string accountNumber) =>
        Random.Shared.Next(1000, 5000);
}
