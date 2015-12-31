using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Models;
using Courses.Models.Repositories;
namespace Courses.Buisness.Account
{
    public interface IAccountService
    {
        IEnumerable<Models.Account> GetAccounts(int page, int pageSize, List<Filtering.FieldFilter> fieldFilter, Filtering.SortFilter sortFilter);

    }
    public class AccountService : IAccountService
    {
        /// <summary>
        /// Неизменяемое поле
        /// </summary>
        private readonly IAccountRepository repository;
        /// <summary>
        /// Внедрение конструктора. Пример использования паттернов Dependecy Injection
        /// </summary>
        /// <param name="repository"></param>
        public AccountService(Models.Repositories.IAccountRepository repository)
        {
            ///Guard Condition
            if (repository != null)
            {
                this.repository = repository;
            }
            else
                throw new ArgumentNullException();
        }

        public IEnumerable<Models.Account> GetAccounts(int page, int pageSize, List<Filtering.FieldFilter> fieldFilter, Filtering.SortFilter sortFilter)
        {
            throw new NotImplementedException();
        }
    }
}
