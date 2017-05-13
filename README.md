# EntityFramework.Testing.FakeItEasy

<p align="center">
    <a href="https://ci.appveyor.com/project/mgibas/entityframework-testing-fakeiteasy/branch/master">
        <img src="https://ci.appveyor.com/api/projects/status/github/mgibas/entityframework.testing.fakeiteasy?branch=master&svg=true"></img>
    </a>
    <a href="https://www.gitcheese.com/donate/users/530319/repos/26915270">
        <img src="https://s3.amazonaws.com/gitcheese-ui-master/images/badge.svg"></img>
    </a>
    <a href="https://www.nuget.org/packages/EntityFramework.Testing.FakeItEasy/">
        <img src="https://img.shields.io/nuget/v/EntityFramework.Testing.FakeItEasy.svg?style=flat-square"></img>
    </a>
</p>

Simple EntityFramework FakeItEasy utility class - feel free to contribute!

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
