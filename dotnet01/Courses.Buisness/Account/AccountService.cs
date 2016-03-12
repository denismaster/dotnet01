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
    
    public class AccountService : IAccountService
    {
        /// <summary>
        /// Репозиторий, используемый сервисом
        /// </summary>
        private readonly IAccountRepository repository;
        /// <summary>
        /// Фабрика фильтров
        /// </summary>
        private readonly Filtering.IFilterFactory<Models.User> filterFactory;
        /// <summary>
        /// Внедрение конструктора. Пример использования паттернов Dependecy Injection
        /// </summary>
        /// <param name="repository"></param>
        public AccountService(Models.Repositories.IAccountRepository repository, Filtering.IFilterFactory<Models.User> filterFactory)
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
        public AccountCollectionViewModel GetAccounts(int page, int pageSize, 
            List<Filtering.FieldFilter> fieldFilters = null,
            Filtering.SortFilter sortFilter = null)
        {
            IEnumerable<AccountViewModel> accounts;
            int total;
            if (fieldFilters != null && sortFilter != null)
            {
                var newSortFilter = new SortFilter() { SortOrder = sortFilter.SortOrder };
                var expression = filterFactory.GetFilterExpression(fieldFilters);
                accounts =  repository.Get(page, pageSize, expression, newSortFilter).Select(Convert);
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
        private User Convert(AccountViewModel c)
        {
            return new User()
            {
                Id = c.Id,
                Login = c.Login,
                Email = c.Email,
                PasswordHash = c.Password,
                Role = c.Role,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Status = System.Convert.ToByte(c.IsActive)
            };
        }
        private AccountViewModel Convert(User c)
        {
            return new AccountViewModel()
            {
                Id = c.Id,
                Login = c.Login,
                Email = c.Email,
                Password = c.PasswordHash,
                Role = c.Role,
                IsActive = System.Convert.ToBoolean(c.Status)
            };
        }
    }
}
