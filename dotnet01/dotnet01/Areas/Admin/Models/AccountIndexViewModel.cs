using System.Collections.Generic;
namespace dotnet01.Areas.Admin.Models
{
    public class AccountIndexViewModel
    {
        public IEnumerable<Account> Accounts {get; set;}
        public PageInfo PageInfo { get; set; }
    }
}