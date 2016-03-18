using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Models;
using Courses.Models.Repositories;
using System.Data.Entity;

namespace Courses.DAL
{
    class EmailQueueRepository:IEmailQueueRepository
    {
        DatabaseContext context = new DatabaseContext();

        public IEnumerable<Models.EmailQueue> Get()
        {
            return context.EmailQueues.AsEnumerable();
        }

        public IEnumerable<Models.EmailQueue> Get(int page, int pageSize, Func<Models.EmailQueue, bool> expression, SortFilter sortFilter)
        {
            return null;
        }
        public IEnumerable<Models.EmailQueue> Get(int page, int pageSize, Func<Models.EmailQueue, bool> expression)
        {
            return context.EmailQueues.Where(expression).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public Models.EmailQueue Get(int id)
        {
            return context.EmailQueues.Find(id);
        }

        public void Add(Models.EmailQueue entity)
        {
            context.EmailQueues.Add(entity);
        }

        public void Update(Models.EmailQueue entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }

        public void Delete(Models.EmailQueue entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }


        public void SaveChanges()
        {
            context.SaveChanges();
        }


        public int Count(Func<Models.EmailQueue, bool> expression)
        {
            return context.EmailQueues.Where(expression).Count();
        }

        public EmailQueue GetOnlyOne()
        {
            return context.EmailQueues.FirstOrDefault();
        }
        public void Dispose()
        {
            context.Dispose();
        }
        public void ClearTable()
        {
            context.EmailQueues.RemoveRange(Get());
        }
    }
}
