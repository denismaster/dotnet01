using System;
using System.Collections.Generic;

namespace Courses.Models.Repositories
{
    public interface IEventRepository : IRepository<Event>, IDisposable
    {
        IEnumerable<Models.Event> Get(int page, int pageSize, Func<Models.Event, bool> expression, SortFilter sortFilter);

    }
}
