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
            IEnumerable<Account> Get(int page,int pageSize);
            Account Get(int id);
            IEnumerable<Account> Get(int page,int pageSize,Expression<Func<Account, bool>> predicate);
            void Add(Account account);
            int Count();
            void Edit(Account account);
            void Delete(Account account);
            void SaveChanges();
            }

    public class AccountRepository : IAccountRepository
    {
        AccountContext database = new AccountContext();
        IEnumerable<Account> accountsPerPages;

        #region pagination
        public  IEnumerable<Account> Get(int page,int pageSize)
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