using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Models.Repositories;
using System.Data.Entity;
namespace Courses.DAL
{
    /// <summary>
    /// Реализация репозитория для работы с аккаунтами
    /// </summary>
    public class AccountRepository:IAccountRepository
    {
        /// <summary>
        /// Контекст Entity Framework, используем для работы с БД
        /// </summary>
        AccountContext context = new AccountContext();

        public IEnumerable<Models.Account> Get()
        {
            return context.Account.AsEnumerable();
        }

        public IEnumerable<Models.Account> Get(int page, int pageSize, Func<Models.Account, bool> expression)
        {
            //временное решение
            return context.Account.Where(expression).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public IEnumerable<Models.Account> Get(int page, int pageSize, Func<Models.Account, bool> expression, SortFilter sortFilter)
        {
            if (String.IsNullOrWhiteSpace(sortFilter.SortOrder))
            {
                return context.Account.Where(expression).OrderBy(s => s.Id).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
            switch (sortFilter.SortOrder)
            {
                case "LogIn":
                    return context.Account.Where(expression).OrderBy(s => s.Login).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "LogInDesc":
                    return context.Account.Where(expression).OrderByDescending(s => s.Login).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "Mail":
                    return context.Account.Where(expression).OrderBy(s => s.Mail).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "MailDesc":
                    return context.Account.Where(expression).OrderByDescending(s => s.Mail).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "Role":
                    return context.Account.Where(expression).OrderBy(s => s.Role).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "RoleDesc":
                    return context.Account.Where(expression).OrderByDescending(s => s.Role).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                default:
                    return context.Account.Where(expression).OrderBy(s => s.Id).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
        }

        public Models.Account Get(int id)
        {
            return context.Account.Find(id);
        }

        public void Add(Models.Account entity)
        {
            context.Account.Add(entity);
        }

        public void Update(Models.Account entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }

        public void Delete(Models.Account entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }


        public void SaveChanges()
        {
            context.SaveChanges();
        }


        public int Count(Func<Models.Account, bool> expression)
        {
            return context.Account.Where(expression).Count();
        }
    }
}
