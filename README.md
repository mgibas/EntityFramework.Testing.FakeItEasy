EntityFramework.Testing.FakeItEasy
==================================

Simple EntityFramework FakeItEasy utility class - fell free and contribute!

Getting Started:

- Creating fake DbSet<T>:
```csharp
var fakeDbSet = A_EF.FakeDbSet(new List<Model>{...});
A.CallTo(() => context.Models).Returns(fakeDbSet);
```
