using System;
using System.Collections.Generic;

namespace Courses.Models.Repositories
{
    public interface IOrderRepository : IRepository<Order>, IDisposable
    {
        IEnumerable<Models.Order> Get(int page, int pageSize, Func<Models.Order, bool> expression, SortFilter sortFilter);

    }
}
