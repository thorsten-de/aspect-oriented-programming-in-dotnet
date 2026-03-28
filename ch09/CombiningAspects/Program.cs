

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


var budgetService = container.GetInstance<IBudgetService>();
decimal budget = budgetService.GetBudgetForAccount("blabla");

Console.WriteLine($"blabla has budget: {budget:C}");


