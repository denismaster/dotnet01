using Courses.Models;
using Courses.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
namespace Courses.DAL
{
    public class EventRepository : RepositoryBase<Event>, IEventRepository
    {
        public IEnumerable<Models.Event> Get(int page, int pageSize, Func<Models.Event, bool> expression, SortFilter sortFilter)
        {
            if (string.IsNullOrWhiteSpace(sortFilter.SortOrder))
            {
                return entityContext.Where(expression).OrderBy(s => s.Id).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
            switch (sortFilter.SortOrder)
            {

                case "CreatedDate":
                    return entityContext.Where(expression).OrderBy(s => s.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "CreatedDateDesc":
                    return entityContext.Where(expression).OrderByDescending(s => s.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                default:
                    return entityContext.Where(expression).OrderBy(s => s.Id).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
        }

    }
}
