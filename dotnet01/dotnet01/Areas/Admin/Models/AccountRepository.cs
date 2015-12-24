using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace dotnet01.Areas.Admin.Models
{
    
    public interface IAccountRepository
            {
            IEnumerable<Account> Get(int page,int pageSize);
            Account Get(int id);
            IEnumerable<Account> Get(Func<Account, bool> predicate, int page,int pageSize);
            void Add(Account account);
            void Edit(Account account);
            void Delete(Account account);
            void SaveChanges();
            }
    public class AccountRepository : IAccountRepository
    {
        AccountContext database = new AccountContext();
        IEnumerable<Account> accountsPerPages;

        #region pagination
        public  IEnumerable<Account> Get(int page = 1,int pageSize =3)
        {
            try {
                accountsPerPages = database.Account.
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
      public IEnumerable<Account> Get(Func<Account, bool> predicate, int page = 1, int pageSize = 3)
        {
            try {
                accountsPerPages = database.Account.
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