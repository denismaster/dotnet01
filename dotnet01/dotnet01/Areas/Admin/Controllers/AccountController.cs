using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using dotnet01.Areas.Admin.Models;
using System.Data.Entity;
using dotnet01.Areas.Admin.Controllers;

namespace dotnet01.Areas.Admin.Controllers
{

    public class AccountController : Controller
    {
        IAccountRepository repository;
        
        public AccountController()
        {
            repository = new AccountRepository(); 
        }
        public ActionResult New()
        {

            //Create AccountNewViewModel and send it to View via parameter
            return View();
        }
        public ActionResult Edit(int id)
        {
            //Create AccountEditViewModel and send it to View via parameter
            //Actions are similar, can use only one model
            return View();
        }
        public ActionResult Index(int page = 1)
        {
            
            int pageSize = 3;
            int Total = repository.Count();           
                
            IEnumerable<Account> accountsPerPages = repository.Get(page, pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Total };
            AccountIndexViewModel ivm = new AccountIndexViewModel() { PageInfo = pageInfo, Accounts = accountsPerPages };
            ViewBag.Accounts = accountsPerPages;
            
            //Create AccountIndexViewModel and send it to View via parameter
            return View(ivm);
        }
    }
}