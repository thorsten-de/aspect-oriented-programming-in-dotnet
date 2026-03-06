# My take on AOP in .NET - Practical Aspect-Oriented Programming

Within this repository, I will code along the examples in the book [AOP in .NET](https://www.manning.com/books/aop-in-net)
by Matthew D. Growes. As it was published in 2013, years before .NET Framework shifted to Core, we probably face
some issues using .NET 10. Another issue might the that [PostSharp](https://www.postsharp.net/) has been superseded
by [MetaLama](https://metalama.net/). 

My intent is to get a feeling what benefits AOP brings to current .NET development, and I will
try to adapt the code examples to .NET 10 and MetaLama while walking through the book.

The versions I use are:

- [PostSharp 2026.0.5 from nuget](https://www.nuget.org/packages/PostSharp)
- [Metalama.Framework 2026.0.16 on nuget](https://www.nuget.org/packages/Metalama.Framework)

You can find the original code examples in the authors [AOPinNET repository on GitHub](https://github.com/mgroves/AOPinNET).

### What has changed using PostSharp in 2026

- Use PostSharps own `[PSerializable]` attribute, as `[Serializable]` depends on `BinaryFormatter` that is considered
  insecure. See [the explanation in postsharp documentation](https://doc.postsharp.net/deploymentconfiguration/deployment/binary-formatter-security) for more information.

### Using Metalama with Visual Studio Code

The Metalama documentation provides some helpful hints for [Configuring VS Code](https://doc.metalama.net/conceptual/using/ide/vs-code). For example,
Roslyn analyzers must be enabled by setting `dotnet.backgroundAnalysis.analyzerDiagnosticScope` accordingly.

## The Acme Car Rental Example

#### Functional / Business Requirements


In the Example, we have two main sets of functional / business requirements to meet:

- Accruing loyaty points for:
	- Customers earn _1 point_ per day for each **car** they rent.
	- Customers renting a **luxury car** earn _2 points_ per day.
- Spend loyalty points:
	- Redeem *10 points* to get a *free rental day*.
	- Redeem *15 points* to get a *free luxury rental day*.


#### Non-Functional / Cross-Cutting Concerns

Additionally, some technical concerns must be addresssed: 
- Logging
- Validate all input as it originates from different UI clients. We defend our code against edge cases and invalid arguments.
- Keep data consistent with transactions
- Make connections more resilient with retry policies
- Handle excetptions

### Process

1. Write the code without using AOP. This is Section 2.2 in the book and found in the section/2.2-life-without-aop branch

## Differences in my implementation of Section 2.2

### Write xunit tests instead of a console program
We atually write tests for the business logic in section 2.2.2. Therefore, instead of a console application,
a new project `AcmeCarRental.Tests` is added. We use
- xUnit testing framework
- `FakeLogger` provided by `Microsoft.Extensions.Diagnostics.Testing` package
- `Moq` for mocking dependencies, where appropriate

Besides faking the `ILogger`, we need to fake other components to make them testable:

- A `FakeTransactionScope` manager can sense when we don't enter or complete a transaction scope properly
- A `FlakyDataService` fake implementation for `ILoyaltyDataService` that can simulate failures