using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Models;
using Courses.DAL;
using Courses.Models.Repositories;
using Courses.Buisness;
namespace TestConsole
{

/*
¨¨¨¨¨¨¨¨¨★ 
¨¨¨¨¨¨¨¨¨**
¨¨¨¨¨¨¨¨¨*o*
¨¨¨¨¨¨¨¨*♥*o*
¨¨¨¨¨¨¨***o***
¨¨¨¨¨¨**o**♥*o*
¨¨¨¨¨**♥**o**o**
¨¨¨¨**o**♥***♥*o*
¨¨¨*****♥*o**o****
¨¨**♥**o*****o**♥**
¨******o*****♥**o***
****o***♥**o***o***♥ *
¨¨¨¨¨____!_!____
¨¨¨¨¨\_________/¨¨ 
 С НОВЫМ ГОДОМ, ДРУЗЬЯ!)
    */
    class Program
    {
        static void Main(string[] args)
        {
            IAccountRepository repository = new AccountRepository();
            Courses.Buisness.Account.IAccountService accountService = new Courses.Buisness.Account.AccountService
                (repository, new Courses.Buisness.Filtering.AccountFilterFactory());
            var accounts = accountService.GetAccounts(2, 2, new List<Courses.Buisness.Filtering.FieldFilter>(), new Courses.Buisness.Filtering.SortFilter()
                {
                    SortOrder = "ASC"
                });
            foreach (var account in accounts)
            {
                Console.WriteLine("{0},{1}", account.Id, account.Login);
            }
        }
    }
}
