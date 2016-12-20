using FakeItEasy;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EntityFramework.FakeItEasy
{
    public static class Aef
    {
        public static DbSet<T> FakeDbSet<T>(IList<T> data) where T : class
        {
            var fakeDbSet = A.Fake<DbSet<T>>(b =>
            {
                b.Implements(typeof(IQueryable<T>));
                b.Implements(typeof(IDbAsyncEnumerable<T>));
            });


            A.CallTo(() => fakeDbSet.Include(A<string>._)).Returns(fakeDbSet);

            QueryableSetUp(fakeDbSet, data);
            CollectionSetUp(fakeDbSet, data);
            DbQuerySetUp(fakeDbSet);

            return fakeDbSet;
        }

        public static DbSet<T> FakeDbSet<T>(IEnumerable<T> data) where T : class
        {
            var fakeDbSet = A.Fake<DbSet<T>>(b =>
            {
                b.Implements(typeof(IDbAsyncEnumerable<T>));
                b.Implements(typeof(IQueryable<T>));
                b.Implements(typeof(DbQuery<T>));

            });
            A.CallTo(() => fakeDbSet.Include(A<string>._)).Returns(fakeDbSet);

            QueryableSetUp(fakeDbSet, data);

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

        private static void QueryableSetUp<T>(IQueryable<T> fakeDbSet, IEnumerable<T> data) where T : class
        {
            A.CallTo(() => fakeDbSet.Provider).ReturnsLazily(l => (new TestDbAsyncQueryProvider<T>(data.AsQueryable().Provider)));
            A.CallTo(() => fakeDbSet.Expression).ReturnsLazily(l => data.AsQueryable().Expression);
            A.CallTo(() => fakeDbSet.ElementType).ReturnsLazily(l => data.AsQueryable().ElementType);
            A.CallTo(() => fakeDbSet.GetEnumerator()).ReturnsLazily(l => data.AsQueryable().GetEnumerator());
            A.CallTo(() => ((IDbAsyncEnumerable<T>)fakeDbSet).GetAsyncEnumerator()).Returns(new TestDbAsyncEnumerator<T>(data.GetEnumerator()));

        }



        private static void CollectionSetUp<T>(DbSet<T> fakeDbSet, ICollection<T> data) where T : class
        {

            A.CallTo(() => fakeDbSet.Add(A<T>._)).Invokes((T item) => data.Add(item));
            A.CallTo(() => fakeDbSet.AddRange(A<IEnumerable<T>>._)).Invokes((IEnumerable<T> items) =>
            {
                foreach (var item in items)
                    data.Add(item);
            });
            A.CallTo(() => fakeDbSet.Remove(A<T>._)).Invokes((T item) => data.Remove(item));
            A.CallTo(() => fakeDbSet.RemoveRange(A<IEnumerable<T>>._)).Invokes((IEnumerable<T> items) =>
            {
                foreach (var item in items.ToList())
                    data.Remove(item);
            });
        }

        private static void DbQuerySetUp<T>(DbQuery<T> fakeDbQuery)
        {
            A.CallTo(() => fakeDbQuery.AsNoTracking()).Returns(fakeDbQuery);

        }
    }


    #region "implementation of IDbAsyncQueryProvider"
    /*
     * So here we must change our behaviour for the Provider property. 
     * I have made use of a class provided by MSDN that wraps up our fakeIQueryable to provide 
     * an implementation of IDbAsyncQueryProvider:
     * https://msdn.microsoft.com/en-us/library/dn314429%28v=vs.113%29.aspx
     */

    internal class TestDbAsyncQueryProvider<TEntity> : IDbAsyncQueryProvider
    {
        private readonly IQueryProvider _inner;

        internal TestDbAsyncQueryProvider(IQueryProvider inner)
        {
            _inner = inner;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return new TestDbAsyncEnumerable<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new TestDbAsyncEnumerable<TElement>(expression);
        }

        public object Execute(Expression expression)
        {
            return _inner.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return _inner.Execute<TResult>(expression);
        }

        public Task<object> ExecuteAsync(Expression expression, CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute(expression));
        }

        public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute<TResult>(expression));
        }
    }

    internal class TestDbAsyncEnumerable<T> : EnumerableQuery<T>, IDbAsyncEnumerable<T>, IQueryable<T>
    {
        public TestDbAsyncEnumerable(IEnumerable<T> enumerable)
            : base(enumerable)
        { }

        public TestDbAsyncEnumerable(Expression expression)
            : base(expression)
        { }

        public IDbAsyncEnumerator<T> GetAsyncEnumerator()
        {
            return new TestDbAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
        {
            return GetAsyncEnumerator();
        }

        IQueryProvider IQueryable.Provider
        {
            get { return new TestDbAsyncQueryProvider<T>(this); }
        }
    }

    internal class TestDbAsyncEnumerator<T> : IDbAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        public TestDbAsyncEnumerator(IEnumerator<T> inner)
        {
            _inner = inner;
        }

        public void Dispose()
        {
            _inner.Dispose();
        }

        public Task<bool> MoveNextAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_inner.MoveNext());
        }

        public T Current
        {
            get { return _inner.Current; }
        }

        object IDbAsyncEnumerator.Current
        {
            get { return Current; }
        }
    }

    #endregion

}

