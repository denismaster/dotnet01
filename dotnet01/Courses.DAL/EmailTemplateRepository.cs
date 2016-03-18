using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Models.Repositories;
using System.Data.Entity;
using Courses.Models;

namespace Courses.DAL
{
    public class EmailTemplateRepository: IEmailTemplateRepoitory
    {
        DatabaseContext context = new DatabaseContext();

        public IEnumerable<EmailTemplate> Get()
        {
            return context.EmailTemplates.AsEnumerable();
        }

        public IEnumerable<Models.EmailTemplate> Get(int page, int pageSize, Func<Models.EmailTemplate, bool> expression, SortFilter sortFilter)
        {
            return null;
        }
        public IEnumerable<Models.EmailTemplate> Get(int page, int pageSize, Func<Models.EmailTemplate, bool> expression)
        {
            return context.EmailTemplates.Where(expression).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public Models.EmailTemplate Get(int id)
        {
            return context.EmailTemplates.Find(id);
        }

        public void Add(Models.EmailTemplate entity)
        {
            context.EmailTemplates.Add(entity);
        }

        public void Update(Models.EmailTemplate entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }

        public void Delete(Models.EmailTemplate entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }


        public void SaveChanges()
        {
            context.SaveChanges();
        }


        public int Count(Func<Models.EmailTemplate, bool> expression)
        {
            return context.EmailTemplates.Where(expression).Count();
        }


        public void Dispose()
        {
            context.Dispose();
        }
        public EmailTemplate GetOnlyOne()
        {
            return context.EmailTemplates.FirstOrDefault();
        }
        public void ClearTable()
        {
            context.EmailTemplates.RemoveRange(Get());
        }
    }
}
