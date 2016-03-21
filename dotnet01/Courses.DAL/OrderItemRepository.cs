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
    class OrderItemRepository:IOrderItemRepository
    {
        DatabaseContext context = new DatabaseContext();

        public IEnumerable<Models.OrderItem> Get()
        {
            return context.OrderItems.AsEnumerable();
        }

        public IEnumerable<Models.OrderItem> Get(int page, int pageSize, Func<Models.OrderItem, bool> expression, SortFilter sortFilter)
        {
            return null;
        }
        public IEnumerable<Models.OrderItem> Get(int page, int pageSize, Func<Models.OrderItem, bool> expression)
        {
            return context.OrderItems.Where(expression).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public Models.OrderItem Get(int id)
        {
            return context.OrderItems.Find(id);
        }

        public void Add(Models.OrderItem entity)
        {
            context.OrderItems.Add(entity);
        }

        public void Update(Models.OrderItem entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }

        public void Delete(Models.OrderItem entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }


        public void SaveChanges()
        {
            context.SaveChanges();
        }


        public int Count(Func<Models.OrderItem, bool> expression)
        {
            return context.OrderItems.Where(expression).Count();
        }

        public OrderItem GetOnlyOne()
        {
            return context.OrderItems.FirstOrDefault();
        }
        public void Dispose()
        {
            context.Dispose();
        }
        public void ClearTable()
        {
            context.OrderItems.RemoveRange(Get());
        }
    }
}
