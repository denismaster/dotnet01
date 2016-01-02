using System.Collections.Generic;
using Models2 = Courses.Models;
namespace dotnet01.Areas.Admin.Models
{
    public class AccountIndexViewModel
    {
        public IEnumerable<Models2.Account> Accounts {get; set;}
        public PageInfo PageInfo { get; set; }
    }
}