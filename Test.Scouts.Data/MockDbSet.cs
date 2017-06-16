using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Test.Scouts.Data
{
    public static class MockDbSet
    {
        public static Mock<DbSet<T>> CreateDbSetMock<T>(this List<T> list) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(() => list.AsQueryable().Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(() => list.AsQueryable().Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(() => list.AsQueryable().ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => list.GetEnumerator());
            mockSet.Setup(m => m.Add(It.IsAny<T>())).Callback((T x) => list.Add(x));
            mockSet.Setup(m => m.AddRange(It.IsAny<IEnumerable<T>>())).Callback((IEnumerable<T> x) => list.AddRange(x));
            mockSet.Setup(m => m.Remove(It.IsAny<T>())).Callback((T x) => list.Remove(x));

            return mockSet;
        }
    }
}
