<a href="https://www.gitcheese.com/app/#/projects/b6a20f9c-68b0-4c50-8fc2-e8c33e0baa51/pledges/create" target="_blank" style="float:left;" > <img src="https://api.gitcheese.com/v1/projects/b6a20f9c-68b0-4c50-8fc2-e8c33e0baa51/badges" width="200px" /> </a>
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
