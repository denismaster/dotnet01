using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Courses.Models;
namespace Courses.DAL
{
    public class AccountContext : DbContext
    {
        public AccountContext() :
            base("AccountsDatabase")
        { }

        public DbSet<Account> Account { get; set; }
    }
}
