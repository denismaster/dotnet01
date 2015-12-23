using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace dotnet01.Areas.Admin.Models
{
    public class Account
    {

    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Mail { get; set; }
    public string Role { get; set; }
    }

    public class AccountContext : DbContext
    {
        public AccountContext() :
            base("AccountsDatabase")
        { }
 
        public DbSet<Account> Account { get; set; }
    }
}