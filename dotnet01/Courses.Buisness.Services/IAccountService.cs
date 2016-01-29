using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Models;
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
}
