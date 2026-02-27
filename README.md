# My take on AOP in .NET - Practical Aspect-Oriented Programming

Within this repository, I will code along the examples in the book [AOP in .NET](https://www.manning.com/books/aop-in-net)
by Matthew D. Growes. As it was published in 2013, years before .NET Framework shifted to Core, we probably face
some issues using .NET 10. Another issue might the that [PostSharp](https://www.postsharp.net/) has been superseded
by [MetaLama](https://metalama.net/).

My intent is to get a feeling what benefits AOP brings to current .NET development, and I will
try to adapt the code examples to .NET 10 and MetaLama while walking through the book.

The versions I use are:

- [PostSharp 2026.0.5 from nuget](https://www.nuget.org/packages/PostSharp)

### What has changed using PostSharp in 2026

- Use PostSharps own `[PSerializable]` attribute, as `[Serializable]` depends on `BinaryFormatter` that is considered
  insecure. See [the explanation in postsharp documentation](https://doc.postsharp.net/deploymentconfiguration/deployment/binary-formatter-security) for more information.
