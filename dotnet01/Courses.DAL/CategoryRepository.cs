using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.DAL
{
    class CategoryRepository
    {
        DatabaseContext context = new DatabaseContext();

        public IEnumerable<Models.Category> Get()
        {
            return context.Categories.AsEnumerable();
        }

        public IEnumerable<Models.Category> Get(int page, int pageSize, Func<Models.Category, bool> expression)
        {
            return context.Categories.Where(expression).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public Models.Category Get(int id)
        {
            return context.Categories.Find(id);
        }

        public void Add(Models.Category entity)
        {
            context.Categories.Add(entity);
        }

        public void Update(Models.Category entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }

        public void Delete(Models.Category entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }


        public void SaveChanges()
        {
            context.SaveChanges();
        }


        public int Count(Func<Models.Category, bool> expression)
        {
            return context.Categories.Where(expression).Count();
        }


        public void Dispose()
        {
            context.Dispose();
        }
    }
}
