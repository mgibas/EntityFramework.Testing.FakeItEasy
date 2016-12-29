using EntityFramework.FakeItEasy;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EntityFramework.Testing.FakeItEasy.Tests
{
    [TestFixture]
    public class AefAsyncTests
    {


        [Test]
        public void WhereAsync_ShouldReturnFilteredItems()
        {
            var fakeDbSet = Aef.FakeDbSet(
                    new List<TestModel> {
                        new TestModel { Id = 1, Property = "apple" },
                        new TestModel { Id = 2, Property = "banana" },
                        new TestModel { Id = 3, Property = "Cherry" }
                   });

            var items = fakeDbSet.Where(e => e.Property.Contains("a")).ToListAsync().Result;


            Assert.AreEqual(items.Count(), 2);


        }


        [Test]
        public void FirstOrDefaultAsync_ShouldReturnFilteredSinlgeItem()
        {
            var fakeDbSet = Aef.FakeDbSet(
                    new List<TestModel> {
                        new TestModel { Id = 1, Property = "apple" },
                        new TestModel { Id = 2, Property = "banana" },
                        new TestModel { Id = 3, Property = "Cherry" }
                   });

            var items = fakeDbSet.FirstOrDefaultAsync(e => e.Property.Equals("Cherry")).Result;


            Assert.AreEqual(items.Property, "Cherry");

        }


        [Test]
        public void AnyAsync_ShouldReturnTrue()
        {
            var fakeDbSet = Aef.FakeDbSet(
                    new List<TestModel> {
                        new TestModel { Id = 1, Property = "apple" },
                        new TestModel { Id = 2, Property = "banana" },
                        new TestModel { Id = 3, Property = "Grape" }
                   });

            var result = fakeDbSet.AnyAsync(e => e.Property.Contains("n")).Result;


            Assert.AreEqual(result, true);

        }

        [Test]
        public void AllAsync_ShouldReturnTrue()
        {
            var fakeDbSet = Aef.FakeDbSet(
                    new List<TestModel> {
                        new TestModel { Id = 1, Property = "apple" },
                        new TestModel { Id = 2, Property = "banana" },
                        new TestModel { Id = 3, Property = "Grape" }
                   });

            var result = fakeDbSet.AllAsync(e => e.Property.Contains("a")).Result;


            Assert.AreEqual(result, true);

        }


        [Test]
        public void CountAsync_ShouldReturnListCount()
        {
            var fakeDbSet = Aef.FakeDbSet(
                    new List<TestModel> {
                        new TestModel { Id = 1, Property = "apple" },
                        new TestModel { Id = 2, Property = "banana" },
                        new TestModel { Id = 3, Property = "Grape" }
                   });

            var result = fakeDbSet.CountAsync().Result;


            Assert.AreEqual(result, 3);

        }

    }


}
