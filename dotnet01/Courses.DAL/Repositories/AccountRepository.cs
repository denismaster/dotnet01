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
    public class AccountRepository : RepositoryBase<User>, IAccountRepository
    {
        public IEnumerable<Models.User> Get(int page, int pageSize, Func<Models.User, bool> expression, SortFilter sortFilter)
        {
            if (String.IsNullOrWhiteSpace(sortFilter.SortOrder))
            {
                return entityContext.Where(expression).OrderBy(s => s.Id).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
            switch (sortFilter.SortOrder)
            {
                case "LogIn":
                    return entityContext.Where(expression).OrderBy(s => s.Login).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "LogInDesc":
                    return entityContext.Where(expression).OrderByDescending(s => s.Login).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "Mail":
                    return entityContext.Where(expression).OrderBy(s => s.Email).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "MailDesc":
                    return entityContext.Where(expression).OrderByDescending(s => s.Email).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "Role":
                    return entityContext.Where(expression).OrderBy(s => s.Role).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "RoleDesc":
                    return entityContext.Where(expression).OrderByDescending(s => s.Role).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                default:
                    return entityContext.Where(expression).OrderBy(s => s.Id).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
        }

        public User GetUser(string login, string password)
        {
            return entityContext.Where(a => a.Login == login && a.PasswordHash == password).FirstOrDefault();
        }

        public User GetUserByID(string id)
        {
            var intId = 0;
            if (!int.TryParse(id, out intId))
                return null;
            var result = entityContext.Find(intId);
            return result;
        }

        public User GetUserByName(string email)
        {
            //var allRecords = entityContext.ToList();
            return entityContext.Where(u => u.Login == email).FirstOrDefault();
        }
        public User GetUserByAuthKey(string authKey)
        {
            return entityContext.Where(u => u.AuthKey == authKey).FirstOrDefault();
        }

        public User GetUserByPassword(string username,string password)
        {
            return entityContext.Where(u => u.Login == username && u.PasswordHash == password).FirstOrDefault();
        }

    }
}
