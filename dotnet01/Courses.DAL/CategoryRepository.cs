using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses.Models.Repositories;
using Courses.Models;
using System.Data.Entity;

namespace Courses.DAL
{
    public  class CategoryRepository: ICategoryRepository
    {
        DatabaseContext context = new DatabaseContext();

        public IEnumerable<Models.Category> Get()
        {
            return context.Categories.AsEnumerable();
        }

        //временно нереализовано
        public IEnumerable<Category> Get(int page, int pageSize, Func<Category, bool> expression, SortFilter sortFilter) {
            return null;
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
