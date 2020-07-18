using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace FS.Data.Test.Setup
{
    internal static class MockDbSetFactory
    {
        // Creates a mock DbSet from the specified data.
        public static Mock<DbSet<T>> Create<T>(IEnumerable<T> data) where T : class
        {
            var queryable = data.AsQueryable();
            var mock = new Mock<DbSet<T>>();
            mock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

            mock.Setup(u => u.Add(It.IsAny<T>())).Callback<T>((s) => data.Concat(new[] { s }));

            return mock;
        }
    }
}