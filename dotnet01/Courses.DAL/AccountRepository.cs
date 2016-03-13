using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses.Models.Repositories;
using Courses.Models;
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
        DatabaseContext context = new DatabaseContext();

        public IEnumerable<Models.User> Get()
        {
            return context.Users.AsEnumerable();
        }

        public IEnumerable<Models.User> Get(int page, int pageSize, Func<Models.User, bool> expression)
        {
            //временное решение
            return context.Users.Where(expression).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public IEnumerable<Models.User> Get(int page, int pageSize, Func<Models.User, bool> expression, SortFilter sortFilter)
        {
            if (String.IsNullOrWhiteSpace(sortFilter.SortOrder))
            {
                return context.Users.Where(expression).OrderBy(s => s.Id).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
            switch (sortFilter.SortOrder)
            {
                case "LogIn":
                    return context.Users.Where(expression).OrderBy(s => s.Login).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "LogInDesc":
                    return context.Users.Where(expression).OrderByDescending(s => s.Login).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "Mail":
                    return context.Users.Where(expression).OrderBy(s => s.Email).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "MailDesc":
                    return context.Users.Where(expression).OrderByDescending(s => s.Email).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "Role":
                    return context.Users.Where(expression).OrderBy(s => s.Role).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "RoleDesc":
                    return context.Users.Where(expression).OrderByDescending(s => s.Role).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                default:
                    return context.Users.Where(expression).OrderBy(s => s.Id).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
        }

        public Models.User GetUser(string login, string password)
        {
            return context.Users.Where(a => a.Login == login && a.PasswordHash == password).FirstOrDefault();
        }

        public Models.User Get(int id)
        {
            return context.Users.Find(id);
        }

        public void Add(Models.User entity)
        {
            context.Users.Add(entity);
        }

        public void Update(Models.User entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Models.User entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }


        public void SaveChanges()
        {
            context.SaveChanges();
        }


        public int Count(Func<Models.User, bool> expression)
        {
            return context.Users.Where(expression).Count();
        }

        public User GetUserByID(string id)
        {
            var intId=0;
            if (!int.TryParse(id, out intId))
                return null;
            var result = context.Users.Find(intId);
            return result;
        }

        public User GetUserByName(string email)
        {
            //var allRecords = context.Users.ToList();
            return context.Users.Where(u => u.Login == email).FirstOrDefault();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
