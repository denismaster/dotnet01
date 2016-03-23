using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Models.Repositories
{
    public interface IAccountRepository: IRepository<User>,IDisposable
    {
        //Дополнительные действия, специфичные для аккаунтов.
        User GetUser(string login, string password);
        User GetUserByID(string id);
        User GetUserByName(string username);

        User GetUserByAuthKey(string authKey);
        User GetUserByPassword(string username, string password);
        /// <summary>
        /// Получает все сущности по заданному условию и с заданной сортировкой
        /// </summary>
        /// <param name="expression">Лямбда-выражение, описывающее условие поиска</param>
        /// <param name="sortFilter">Поле и направление сортировки</param>
        /// <returns></returns>
        IEnumerable<User> Get(int page, int pageSize, Func<User, bool> expression, SortFilter sortFilter);
    }
}
