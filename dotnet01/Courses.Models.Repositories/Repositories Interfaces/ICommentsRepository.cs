using System;
using System.Collections.Generic;

namespace Courses.Models.Repositories
{
    public interface ICommentsRepository : IRepository<Comment>, IDisposable
    {
        IEnumerable<Models.Comment> Get(int page, int pageSize, Func<Models.Comment, bool> expression, SortFilter sortFilter);
    }
}
