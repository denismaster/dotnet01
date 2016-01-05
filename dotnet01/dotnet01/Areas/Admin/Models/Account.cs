using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using dotnet01.Areas.Manager.Models;

namespace dotnet01.Areas.Admin.Models
{
    public class Account
    {
        
    public int Id { get; set; }

    [Required(ErrorMessage = "*поле должно быть заполнено")]
    public string Login { get; set; }

    [Required(ErrorMessage = "*поле должно быть заполнено")]
    [StringLength(15, MinimumLength = 5, ErrorMessage = "*длина должна быть от 5 до 15 символов")]
    public string Password { get; set; }

    [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "*неверный адрес")]
    public string Mail { get; set; }

    public string Role { get; set; }

    }

    public class AccountContext : DbContext
    {
        public AccountContext() : base("AccountsDatabase") { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<ManagerAccount> ManagerAccounts { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Rating> Ratings { get; set; }

    }
}