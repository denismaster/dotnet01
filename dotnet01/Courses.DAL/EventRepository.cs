using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses.Models.Repositories;
using Courses.Models;
using System.Data.Entity;
namespace Courses.DAL
{
    class EventRepository: IEventRepository
    {
        DatabaseContext context = new DatabaseContext();

        public IEnumerable<Models.Event> Get()
        {
            return context.Events.AsEnumerable();
        }
        public IEnumerable<Models.Event> Get(int page, int pageSize, Func<Models.Event, bool> expression, SortFilter sortFilter)
        {
            return null;
        }
        public IEnumerable<Models.Event> Get(int page, int pageSize, Func<Models.Event, bool> expression)
        {
            return context.Events.Where(expression).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public Models.Event Get(int id)
        {
            return context.Events.Find(id);
        }

        public void Add(Models.Event entity)
        {
            context.Events.Add(entity);
        }

        public void Update(Models.Event entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }

        public void Delete(Models.Event entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }


        public void SaveChanges()
        {
            context.SaveChanges();
        }


        public int Count(Func<Models.Event, bool> expression)
        {
            return context.Events.Where(expression).Count();
        }


        public void Dispose()
        {
            context.Dispose();
        }
        public void ClearTable()
        {
            context.Events.RemoveRange(Get());
        }
    }
}
