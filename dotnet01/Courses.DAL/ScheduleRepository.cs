using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Models.Repositories;
using Courses.Models;
using System.Data.Entity;

namespace Courses.DAL
{
    class ScheduleRepository:IScheduleRepository
    {
        DatabaseContext context = new DatabaseContext();

        public IEnumerable<Schedule> Get()
        {
            return context.Schedules.AsEnumerable();
        }

        public IEnumerable<Models.Schedule> Get(int page, int pageSize, Func<Models.Schedule, bool> expression, SortFilter sortFilter)
        {
            return null;
        }
        public IEnumerable<Models.Schedule> Get(int page, int pageSize, Func<Models.Schedule, bool> expression)
        {
            return context.Schedules.Where(expression).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public Models.Schedule Get(int id)
        {
            return context.Schedules.Find(id);
        }

        public void Add(Models.Schedule entity)
        {
            context.Schedules.Add(entity);
        }

        public void Update(Models.Schedule entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }

        public void Delete(Models.Schedule entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }


        public void SaveChanges()
        {
            context.SaveChanges();
        }


        public int Count(Func<Models.Schedule, bool> expression)
        {
            return context.Schedules.Where(expression).Count();
        }

        public Schedule GetOnlyOne()
        {
            return context.Schedules.FirstOrDefault();
        }
        public void Dispose()
        {
            context.Dispose();
        }
        public void ClearTable()
        {
            context.Schedules.RemoveRange(Get());
        }
    }
}
