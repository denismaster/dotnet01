using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Linq.Expressions;

namespace dotnet01.Areas.Admin.Models
{
    
    public interface IAccountRepository
            {
        /// <summary>
        /// Возвращает из БД страницу по заданному номеру и размеру страницы, упорядоченную по возрастанию id. 
        /// </summary>
        /// <param name="page"> номер страницы, нумерация с 1</param>
        /// <param name="pageSize"> размер страницы</param>
        IEnumerable<Account> Get(int page,int pageSize);
        /// <summary>
        /// Возвращает из БД информацию о заданном аккаунте по id. При отсутствии аккаунта возвращает null.
        /// </summary>
        /// <param name="id"> id требуемого аккаунта</param>
        Account Get(int id);
        /// <summary>
        /// Возвращает из БД страницу по заданному номеру и размеру страницы, упорядоченную по возрастанию id, с использованием фильтрации.
        /// </summary>
        /// <param name="page"> номер страницы, нумерация с 1</param>
        /// <param name="pageSize"> размер страницы</param>
        /// <param name="predicate"> лямбда-выражение для поиска страницы по заданному фильтру</param>
        IEnumerable<Account> Get(int page,int pageSize,Expression<Func<Account, bool>> predicate);
        /// <summary>
        /// Добавляет аккаунт в БД.
        ///  Для сохранения изменений в БД использовать SaveChanges()
        /// </summary>
        /// <param name="account"> объект сущности для добавления</param>
        void Add(Account account);
        /// <summary>
        /// Возвращает количество аккаунтов, находящихся в БД.
        /// Для сохранения изменений в БД использовать SaveChanges()
        /// </summary>
        int Count();
        /// <summary>
        /// Редактирует аккаунт в БД. Ищет вхождение данного аккаунта в базе и изменяет его.
        /// Передавать только существующую в базе сущность, которая была изменена.
        /// Для сохранения изменений в БД использовать SaveChanges()
        /// </summary>
        /// <param name="account"> объект измененной сущности</param>
        void Edit(Account account);
        /// <summary>
        ///Удаляет сущность из БД. Работает только с существующей в базе сущностью.
        ///  Для сохранения изменений в БД использовать SaveChanges()
        /// </summary>
        /// <param name="account"> объект сущности для удаления</param>
        void Delete(Account account);
        /// <summary>
        ///Не обрабатывает исключения. При вызове нужно перехватывать DataException. 
        /// Сохраняет изменения в БД. 
        /// </summary>
        void SaveChanges();
        /// <summary>
        ///Осуществляет выборку страницы по заданным фильтрам и заданной сортировке.
        /// </summary>
        IEnumerable<Account> Get(int page, int pageSize, List<FieldFilter> fieldFilter, SortFilter sortFilter);

            }

    public class AccountRepository : IAccountRepository
    {
        AccountContext database = new AccountContext();
        IEnumerable<Account> accountsPerPages;

        #region pagination


        public IEnumerable<Account> Get(int page,int pageSize)
        {
            try {
                accountsPerPages = database.Account.OrderBy(acc=>acc.Id).
                    Select(item => item).
                    Skip((page - 1) * pageSize).
                    Take(pageSize).
                    AsEnumerable();
            }
            catch(Exception e)
            {
                Log.Write(e);
            }
            return accountsPerPages;
        }

        public IEnumerable<Account> Get(int page, int pageSize, Expression<Func<Account, bool>> predicate)
        {
            try {
                
                accountsPerPages = database.Account.
                OrderBy(acc => acc.Id).
                Select(item => item).
                Where(predicate).
                Skip((page - 1) * pageSize).
                Take(pageSize).
                AsEnumerable();
            }
            catch(Exception e)
            {
                Log.Write(e);
            }
            return accountsPerPages;
        }
        public IEnumerable<Account> Get(int page, int pageSize, List<FieldFilter> fieldFilter, SortFilter sortFilter)
        { 
            Func<Account,bool> fieldExp = CreateLabmdaFieldFilter(fieldFilter);
            try
            {
                accountsPerPages = database.Account.
                Select(item => item).
                Where(fieldExp).
                Skip((page - 1) * pageSize).
                Take(pageSize).
                AsEnumerable();

                accountsPerPages = GetSorted(sortFilter);
            }
            catch (Exception e)
            {
                Log.Write(e);
            }
            return accountsPerPages;
        }

        private IEnumerable<Account> GetSorted(SortFilter sortFilter)
       {
            if(String.IsNullOrWhiteSpace(sortFilter.SortOrder))
            {
                accountsPerPages.OrderBy(acc => acc.Id);
                return accountsPerPages;
            }

            switch (sortFilter.SortOrder)
            {
                case "LogIn":
                    accountsPerPages = accountsPerPages.OrderBy(acc => acc.Login);
                    break;
                case "LogInDesc":
                    accountsPerPages = accountsPerPages.OrderByDescending(acc => acc.Login);
                    break;
                case "Mail":
                    accountsPerPages = accountsPerPages.OrderBy(acc => acc.Mail);
                    break;
                case "MailDesc":
                    accountsPerPages = accountsPerPages.OrderByDescending(acc => acc.Mail);
                    break;
                case "Role":
                    accountsPerPages = accountsPerPages.OrderBy(acc => acc.Role);
                    break;
                case "RoleDesc":
                    accountsPerPages = accountsPerPages.OrderByDescending(acc => acc.Role);
                    break;
                default:
                    accountsPerPages = accountsPerPages.OrderBy(acc => acc.Id);
                    break;

            }
            return accountsPerPages;
        }

        #endregion

        #region CRUD operations

        public Account Get(int id)
        {
            Account acc = null;
            try
            {
                acc = database.Account.First(item => item.Id == id);
            }
            catch(Exception e)
            {
                Log.Write(e); 
            }
            return acc;
           
        }
 
        public int Count()
        {
            return database.Account.Count();
        }

    
        public void Add(Account account)
        {
            try {
                database.Account.Add(account);
            }
            catch(Exception e)
            {
                Log.Write(e);
            }
        }
  
        public void Edit(Account account) 
        {
            try {
                database.Entry(account).State = EntityState.Modified;
            }
            catch(Exception e)
            {
                Log.Write(e);
            }
            
        }
   
 
        public void Delete(Account account)
      {
            try {
                database.Entry(account).State = EntityState.Deleted;
            }
            catch(Exception e)
            {
                Log.Write(e);
            }
            
      }
        #endregion
 
        public void SaveChanges() 
        {
           database.SaveChanges();            
        }

        public Func<Account,bool> CreateLabmdaFieldFilter(List<FieldFilter> fieldFilterList)
        {
            Func<Account,bool> filterExp = acc=>acc.Id>=0;
            foreach (FieldFilter fieldFilter in fieldFilterList)
            {
                switch (fieldFilter.Name)
                {
                    case "LogIn":
                        filterExp += acc => acc.Login.Trim().ToUpper().Contains(fieldFilter.Value.Trim().ToUpper());
                        break;
                    case "Role":
                        filterExp += acc => acc.Role.Trim().ToUpper().Contains(fieldFilter.Value.Trim().ToUpper());
                        break;
                    case "Mail":
                        filterExp += acc => acc.Mail.Trim().ToUpper().Contains(fieldFilter.Value.Trim().ToUpper());
                        break;
                    default:
                        filterExp += acc => acc.Id >= 0;
                        break;
                }
            }
            return filterExp; 
        }
     

    }
}