using FakeItEasy;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EntityFramework.FakeItEasy
{
    public static class Aef
    {
        public static DbSet<T> FakeDbSet<T>(IEnumerable<T> data) where T : class
        {
            var fakeDbSet = A.Fake<DbSet<T>>(b => b.Implements(typeof(IQueryable<T>)));

            A.CallTo(() => ((IQueryable<T>)fakeDbSet).Provider).Returns(data.AsQueryable().Provider);
            A.CallTo(() => ((IQueryable<T>)fakeDbSet).Expression).Returns(data.AsQueryable().Expression);
            A.CallTo(() => ((IQueryable<T>)fakeDbSet).ElementType).Returns(data.AsQueryable().ElementType);
            A.CallTo(() => ((IQueryable<T>)fakeDbSet).GetEnumerator()).Returns(data.AsQueryable().GetEnumerator());

            return fakeDbSet;
        }

        public static DbSet<T> FakeDbSet<T>(int numberOfFakes) where T : class
        {
            var data = A.CollectionOfFake<T>(numberOfFakes);
            return FakeDbSet(data);
        }

        public static DbSet<T> FakeDbSet<T>() where T : class
        {
            return FakeDbSet(new List<T>());
        }
    }
}
