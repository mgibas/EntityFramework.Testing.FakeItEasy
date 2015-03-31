EntityFramework.Testing.FakeItEasy [![Build status](https://ci.appveyor.com/api/projects/status/uquci4vq25l8yc3n?retina=true)](https://ci.appveyor.com/project/mgibas/entityframework-testing-fakeiteasy)
==================================

Simple EntityFramework FakeItEasy utility class - fell free and contribute!

Getting Started:

- Creating fake DbSet<T>:
```csharp
var fakeDbSet = Aef.FakeDbSet(new List<Model>{...});
A.CallTo(() => context.Models).Returns(fakeDbSet);
```

```csharp
var fakeDbSet =  Aef.FakeDbSet<Model>(55); //55 Model fakes created by FakeItEasy
A.CallTo(() => context.Models).Returns(fakeDbSet);
```

```csharp
var fakeDbSet = Aef.FakeDbSet<Model>(); //Empty collection
A.CallTo(() => context.Models).Returns(fakeDbSet);
```
