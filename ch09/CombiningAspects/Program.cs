using CombiningAspects.Services;
using Lamar;

var container = new Container(x =>
{
    x.Scan(s =>
    {
        s.TheCallingAssembly();
        s.WithDefaultConventions();
    });
});

string accountNumber = "0815";
var budgetService = container.GetInstance<IBudgetService>();

decimal budget = budgetService.GetBudgetForAccount(accountNumber);
Console.WriteLine($"Account {accountNumber} has budget {budget:C}");

// If caching works, we get the same budget, not a random new one
var budgetAgain = budgetService.GetBudgetForAccount(accountNumber);
Console.WriteLine($"Account {accountNumber} has budget {budgetAgain:C}");



