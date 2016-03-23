using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Models;
using System.Data.Entity;
using Courses.Models.Repositories;

namespace Courses.DAL
{
    public class RepositoryBase<T>  where T : DomainObject
    {
        protected DatabaseContext dbContext = new DatabaseContext();
        protected DbSet<T> entityContext;

        protected RepositoryBase()
        {
            entityContext = dbContext.Set<T>();
        }
        public IEnumerable<T> Get()
        {
            return entityContext.AsEnumerable();
        }
        public IEnumerable<T> Get(int page, int pageSize, Func<T, bool> expression)
        {
            return entityContext.Where(expression).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
        }
        public T Get(int id)
        {
            return entityContext.Find(id);
        }
        public void Add(T entity)
        {
            entityContext.Add(entity);
        }
        public void Update(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
        }
        public void Delete(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Deleted;
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public int Count(Func<T, bool> expression)
        {
            return entityContext.Where(expression).Count();
        }

        public void Dispose()
        {
           dbContext.Dispose();
        }
        public T GetOnlyOne()
        {
            return entityContext.FirstOrDefault();
        }
   
    }
}
