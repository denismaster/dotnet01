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

    }
}