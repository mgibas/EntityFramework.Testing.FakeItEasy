using System.Collections.Generic;
using System.Linq;
using EntityFramework.FakeItEasy;
using NUnit.Framework;

namespace EntityFramework.Testing.FakeItEasy.Tests
{
	[TestFixture]
	public class AefTests
	{
		[Test]
		public void Add_ShouldAddItem()
		{
			var fakeDbSet = Aef.FakeDbSet(new List<TestModel> { new TestModel(), new TestModel() });
			var newModel = new TestModel();

			fakeDbSet.Add(newModel);

			CollectionAssert.Contains(fakeDbSet.ToList(), newModel);
		}

		[Test]
		public void Add_ShouldNotThrowWhenIteratingThrough()
		{
			var fakeDbSet = Aef.FakeDbSet(new List<TestModel> { new TestModel(), new TestModel() });

			fakeDbSet.Add(new TestModel());

			foreach (var model in fakeDbSet)
			{
				Assert.Pass();
			}
		}

		[Test]
		public void AddRange_ShouldAddItems()
		{
			var fakeDbSet = Aef.FakeDbSet(new List<TestModel> { new TestModel(), new TestModel() });
			var newModels = new[] { new TestModel(), new TestModel() };

			fakeDbSet.AddRange(newModels);

			CollectionAssert.IsSubsetOf(newModels, fakeDbSet.ToList());
		}

		[Test]
		public void AddRange_ShouldNotThrowWhenIteratingThrough()
		{
			var fakeDbSet = Aef.FakeDbSet(new List<TestModel> { new TestModel(), new TestModel() });

			fakeDbSet.AddRange(new[] { new TestModel(), new TestModel() });

			foreach (var model in fakeDbSet)
			{
				Assert.Pass();
			}
		}

		[Test]
		public void Remove_ShouldRemoveItem()
		{
			var fakeDbSet = Aef.FakeDbSet(new List<TestModel> { new TestModel(), new TestModel() });
			var toRemove = fakeDbSet.First();

			fakeDbSet.Remove(toRemove);

			CollectionAssert.DoesNotContain(fakeDbSet.ToList(), toRemove);
		}

		[Test]
		public void Remove_ShouldNotThrowWhenIteratingThrough()
		{
			var fakeDbSet = Aef.FakeDbSet(new List<TestModel> { new TestModel(), new TestModel() });

			fakeDbSet.Remove(fakeDbSet.First());

			foreach (var model in fakeDbSet)
			{
				Assert.Pass();
			}
		}

		[Test]
		public void RemoveRange_ShouldRemoveItems()
		{
			var fakeDbSet = Aef.FakeDbSet(new List<TestModel>
			{
				new TestModel {Property = "a"},
				new TestModel {Property = "a"},
				new TestModel {Property = "b"}
			});

			fakeDbSet.RemoveRange(fakeDbSet.Where(x => x.Property == "a"));

			Assert.That(fakeDbSet.Count(), Is.EqualTo(1));
		}

		[Test]
		public void Include_ReturnsItSelf()
		{
			var fakeDbSet = Aef.FakeDbSet(new List<TestModel>());

			Assert.AreSame(fakeDbSet, fakeDbSet.Include("asdas"));
		}

		[Test]
		public void AsNoTracking_ReturnsItSelf()
		{
			var fakeDbSet = Aef.FakeDbSet(new List<TestModel>());

			Assert.AreSame(fakeDbSet, fakeDbSet.AsNoTracking());
		}
	}

	public class TestModel
	{
		public string Property { get; set; }
	}
}
