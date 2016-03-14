using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses.Models.Repositories;
using Courses.Models;
using System.Data.Entity;

namespace Courses.DAL
{
    class CommentsRepository: ICommentsRepository
    {
        DatabaseContext context = new DatabaseContext();

        public IEnumerable<Models.Comment> Get()
        {
            return context.Comments.AsEnumerable();
        }

        public IEnumerable<Models.Comment> Get(int page, int pageSize, Func<Models.Comment, bool> expression, SortFilter sortFilter)
        {
            return null;
        }
        public IEnumerable<Models.Comment> Get(int page, int pageSize, Func<Models.Comment, bool> expression)
        {
            return context.Comments.Where(expression).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public Models.Comment Get(int id)
        {
            return context.Comments.Find(id);
        }

        public void Add(Models.Comment entity)
        {
            context.Comments.Add(entity);
        }

        public void Update(Models.Comment entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Models.Comment entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }


        public void SaveChanges()
        {
            context.SaveChanges();
        }


        public int Count(Func<Models.Comment, bool> expression)
        {
            return context.Comments.Where(expression).Count();
        }


        public void Dispose()
        {
            context.Dispose();
        }
    }
}
