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
        /// Репозиторий, используемый сервисом
        /// </summary>
        private readonly IAccountRepository repository;
        /// <summary>
        /// Фабрика фильтров
        /// </summary>
        private readonly Filtering.IFilterFactory<Models.Account> filterFactory;
        /// <summary>
        /// Внедрение конструктора. Пример использования паттернов Dependecy Injection
        /// </summary>
        /// <param name="repository"></param>
        public AccountService(Models.Repositories.IAccountRepository repository, Filtering.IFilterFactory<Models.Account> filterFactory)
        {
            ///Guard Condition
            if (repository == null)
                throw new ArgumentNullException("Repository is null!");
            if (filterFactory == null)
                throw new ArgumentNullException("Filtering Factory is null!");
            this.repository = repository;
            this.filterFactory = filterFactory;
        }
        /// <summary>
        /// Получение аккаунтов на заданной странице с заданными фильтрами.
        /// </summary>
        /// <param name="page">Номер страницы</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <param name="fieldFilters">Список фильтров</param>
        /// <param name="sortFilter">Порядок сортировки</param>
        /// <returns></returns>
        public IEnumerable<Models.Account> GetAccounts(int page, int pageSize, List<Filtering.FieldFilter> fieldFilters, Filtering.SortFilter sortFilter)
        {
            var expression = filterFactory.GetFilterExpression(fieldFilters, sortFilter);
            return repository.Get(page, pageSize, expression);
        }
    }
}
