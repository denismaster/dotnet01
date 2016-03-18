using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses.Models.Repositories;
using Courses.Models;
using System.Data.Entity;

namespace Courses.DAL
{
    class OrderRepository:IOrderRepository
    {
        DatabaseContext context = new DatabaseContext();

        public IEnumerable<Models.Order> Get()
        {
            return context.Orders.AsEnumerable();
        }
        public IEnumerable<Models.Order> Get(int page, int pageSize, Func<Models.Order, bool> expression, SortFilter sortFilter)
        {
            return null;
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

        public void Delete(Models.Order entity)
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
        public Order GetOnlyOne()
        {
            return context.Orders.FirstOrDefault();
        }
       
        public void Dispose()
        {
            context.Dispose();
        }
        public void ClearTable()
        {
            context.Orders.RemoveRange(Get());
        }
    }
}
