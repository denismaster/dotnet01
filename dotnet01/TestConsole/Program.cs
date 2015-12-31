using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Models;
using Courses.DAL;
using Courses.Models.Repositories;
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
            var accounts = repository.Get();
            foreach (var account in accounts)
            {
                Console.WriteLine("{0},{1}", account.Id, account.Login);
            }
            Console.ReadLine();
        }
    }
}
