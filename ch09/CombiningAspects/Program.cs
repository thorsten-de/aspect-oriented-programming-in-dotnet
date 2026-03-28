using CombiningAspects.Services;

try
{
    string accountNumber = "0815";
    var budgetService = ObjectFactory.GetInstance<IBudgetService>();

    decimal budget = budgetService.GetBudgetForAccount(accountNumber);
    Console.WriteLine($"Account {accountNumber} has budget {budget:C}");

    // If caching works, we get the same budget, not a random new one
    var budgetAgain = budgetService.GetBudgetForAccount(accountNumber);
    Console.WriteLine($"Account {accountNumber} has budget {budgetAgain:C}");
}
catch (Exception ex)
{
    Console.WriteLine($"Unable to retrieve budget. Error: {ex.Message}");
}

