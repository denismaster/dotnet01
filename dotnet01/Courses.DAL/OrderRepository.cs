using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Models;
using System.Data.Entity;

namespace Courses.DAL
{
    class OrderRepository
    {
        DatabaseContext context = new DatabaseContext();

        public IEnumerable<Models.Order> Get()
        {
            return context.Orders.AsEnumerable();
        }

        public IEnumerable<Models.Order> Get(int page, int pageSize, Func<Models.Order, bool> expression)
        {
            return context.Orders.Where(expression).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
        }
        public Models.Order Get(int id)
        {
            return context.Orders.Find(id);
        }

        public void Add(Models.Order entity)
        {
            context.Orders.Add(entity);
        }

        public void Update(Models.Order entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }

        public void Delete(Models.User entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }


        public void SaveChanges()
        {
            context.SaveChanges();
        }


        public int Count(Func<Models.Order, bool> expression)
        {
            return context.Orders.Where(expression).Count();
        }

       
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
