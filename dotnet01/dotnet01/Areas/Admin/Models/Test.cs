using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dotnet01.Models;
using System.IO;
namespace dotnet01.Areas.Admin.Models
{
    public  class Test
    {
        Func<Account, bool> predicate;

        bool func(Account acc)
        {
            if (acc.Id == 2) return true;
            else return false;
        }
        public void start()
        {
            IAccountRepository repository = new AccountRepository();
           IEnumerable<Account> accs=  repository.Get(func, 1, 3);
            StreamWriter file = new StreamWriter("B:\\CourseRepository\\TestFile.txt");
            foreach (var acc in accs)
            {
                file.Write(acc.Id + " " + acc.Login + " " + acc.Mail + " " + acc.Role);
                file.WriteLine();
            }
            file.Close();
            /*
                        using (AccountContext db = new AccountContext())
                        {
                            var accounts = db.Account.ToList();

                            StreamWriter file = new StreamWriter("B:\\CourseRepository\\TestFile.txt");
                            foreach (var acc in accounts)
                            {
                                file.Write(acc.Id + " " + acc.Login + " " + acc.Mail + " " + acc.Role);
                                file.WriteLine();
                            }
                            file.Close();
                            Account newAcc = new Account();
                            newAcc.Id  = 10500;
                            newAcc.Login = "1499";
                            newAcc.Mail = "999999";
                            newAcc.Password = "99999";
                            newAcc.Role = "9999";
                            db.Account.Add(newAcc);
                            db.SaveChanges();
                        }
                        */
        }
    }
}