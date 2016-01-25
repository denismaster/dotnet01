using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Models;
using Courses.Models.Repositories;
using Courses.ViewModels;
namespace Courses.Buisness
{
    public interface IAccountService
    {
        /// <summary>
        /// Возвращает список аккаунтов. 
        /// TODO:Желательно возвращать готовые ViewModels, но это пока неважно.
        /// </summary>
        AccountCollectionViewModel GetAccounts(int page, int pageSize,
            List<Filtering.FieldFilter> fieldFilter = null, Filtering.SortFilter sortFilter = null);
        /// <summary>
        /// Получение одного аккаунта
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AccountViewModel GetByID(int id);
        /// <summary>
        /// Добавление аккаунта. 
        /// </summary>
        /// <param name="account"></param>
        void Add(AccountViewModel account);
        /// <summary>
        /// Обновление данных аккаунта
        /// </summary>
        /// <param name="account"></param>
        void Edit(AccountViewModel account);
        /// <summary>
        /// Удаление аккаунта
        /// </summary>
        /// <param name="account"></param>
        void Delete(AccountViewModel account);
        /// <summary>
        /// Сохранение изменений в репозитории
        /// </summary>
        void SaveChanges();
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
        public AccountCollectionViewModel GetAccounts(int page, int pageSize, List<Filtering.FieldFilter> fieldFilters = null,
            Filtering.SortFilter sortFilter = null)
        {
            IEnumerable<AccountViewModel> accounts;
            int total;
            if (fieldFilters != null && sortFilter != null)
            {
                var expression = filterFactory.GetFilterExpression(fieldFilters);
                accounts =  repository.GetSorted(page, pageSize, expression, sortFilter.SortOrder).Select(Convert);
                total = repository.Count(expression);
            }
            else
            {
            accounts =  repository.Get(page, pageSize, x => true).Select(Convert);
                total = repository.Count(x=>true);
            }
            var pageInfo = new PageInfo()
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = total
            };
            return new AccountCollectionViewModel(){Accounts = accounts, PageInfo = pageInfo};
        }

        /// <summary>
        /// Получение информации об аккаунте по его идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AccountViewModel GetByID(int id)
        {
            var account = repository.Get(id);
            return (account == null) ? null : Convert(account);
        }
        /// <summary>
        /// Добавление аккаунта в репозиторий
        /// </summary>
        /// <param name="account"></param>
        public void Add(AccountViewModel account)
        {
            repository.Add(Convert(account));
        }
        /// <summary>
        /// Обновление аккаунта
        /// </summary>
        /// <param name="account"></param>
        public void Edit(AccountViewModel account)
        {
            repository.Update(Convert(account));
        }
        /// <summary>
        /// Удаление аккаунта
        /// </summary>
        /// <param name="account"></param>
        public void Delete(AccountViewModel account)
        {
            repository.Delete(Convert(account));
        }
        /// <summary>
        /// Сохранение изменений
        /// </summary>
        public void SaveChanges()
        {
            repository.SaveChanges();
        }
        /// <summary>
        /// Конвертационные функции
        /// </summary>
        private Account Convert(AccountViewModel c)
        {
            return new Account()
            {
                Id = c.Id,
                Login = c.Login,
                Mail = c.Mail,
                Password = c.Password,
                Role = c.Role
            };
        }
        private AccountViewModel Convert(Account c)
        {
            return new AccountViewModel()
            {
                Id = c.Id,
                Login = c.Login,
                Mail = c.Mail,
                Password = c.Password,
                Role = c.Role
            };
        }
    }
}
