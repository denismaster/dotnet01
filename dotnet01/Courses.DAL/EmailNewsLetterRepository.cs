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
    class EmailNewsLetterRepository:IEmailNewsletterRepository
    {

        DatabaseContext context = new DatabaseContext();

        public IEnumerable<EmailNewsletter> Get()
        {
            return context.EmailNewsLetters.AsEnumerable();
        }

        public IEnumerable<Models.EmailNewsletter> Get(int page, int pageSize, Func<Models.EmailNewsletter, bool> expression, SortFilter sortFilter)
        {
            return null;
        }
        public IEnumerable<Models.EmailNewsletter> Get(int page, int pageSize, Func<Models.EmailNewsletter, bool> expression)
        {
            return context.EmailNewsLetters.Where(expression).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public Models.EmailNewsletter Get(int id)
        {
            return context.EmailNewsLetters.Find(id);
        }

        public void Add(Models.EmailNewsletter entity)
        {
            context.EmailNewsLetters.Add(entity);
        }

        public void Update(Models.EmailNewsletter entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }

        public void Delete(Models.EmailNewsletter entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }


        public void SaveChanges()
        {
            context.SaveChanges();
        }


        public int Count(Func<Models.EmailNewsletter, bool> expression)
        {
            return context.EmailNewsLetters.Where(expression).Count();
        }


        public void Dispose()
        {
            context.Dispose();
        }
        public EmailNewsletter GetOnlyOne()
        {
            return context.EmailNewsLetters.FirstOrDefault();
        }
        public void ClearTable()
        {
            context.EmailNewsLetters.RemoveRange(Get());
        }
    }
}
