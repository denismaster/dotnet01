using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dotnet01.Areas.Admin.Models
{
      //TODO:define IAccountRepository here
    public interface IAccountRepository
            {
            IEnumerable<Account> Get(int page,int pageSize);
            Account GetById(int id);
            IEnumerable<Account> Get(Func<Account, bool> func, int page,int pageSize);
            void Add(Account account);
            void Edit(Account account);
            void Delete(Account account);
            void SaveChanges();
            }
    public class AccountRepository :IAccountRepository
    {
        AccountContext database;
        IEnumerable<Account> accounts;
      
        public AccountRepository()
        {
            database = new AccountContext();
        }
        #region pagination
      public  IEnumerable<Account> Get(int page = 1,int pageSize =3)
        {
            
            accounts = database.Account.Select(item => item).ToList().AsEnumerable();
            IEnumerable<Account> accountsPerPages = accounts.Skip((page - 1) * pageSize).Take(pageSize);
            return accountsPerPages;
        }
      public IEnumerable<Account> Get(Func<Account, bool> func, int page = 1, int pageSize = 3)
        {
            accounts = database.Account.Select(item => item).Where(func).ToList().AsEnumerable();
            IEnumerable<Account> accountsPerPages = accounts.Skip((page - 1) * pageSize).Take(pageSize);
            return accountsPerPages;
        }
    #endregion

        #region CRUD operations
      public Account GetById(int id)
        {
            Account acc = database.Account.First(item => item.Id == id);
            return acc;
        }
      public void Add(Account account)
        {
                database.Account.Add(account);
                SaveChanges();
        }
      public void Edit(Account account) 
        {
            /*Account accToEdit = GetById(account.Id);
            Account accToPush = new Account();
           
            if(accToEdit!=null)
            {
                Delete(accToEdit);

            }*/
            throw new NotImplementedException();
        }
      public void Delete(Account account)
      {
            Account accToDelete = GetById(account.Id);
            if(accToDelete!=null)
            {
                database.Account.Remove(accToDelete);
                SaveChanges();
            }
        }
        #endregion
      public void SaveChanges() 
        { 
            database.SaveChanges();
        }

    }
}