using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using dotnet01.Areas.Admin.Models;
using System.Data.Entity;
using dotnet01.Areas.Admin.Controllers;
using System.Data;

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
        public ActionResult Edit(Account account)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repository.Edit(account);
                    repository.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                Log.Write(e);
                ModelState.AddModelError("", "Unable to save changes");              
            }

            //Create AccountEditViewModel and send it to View via parameter
            //Actions are similar, can use only one model
            return View();
        }
        public ActionResult Index(int page = 1)
        {
            
            int pageSize = 3;
            int Total = repository.Count();           
            //получаем страницу, заданную в параметре из базы данных 
            IEnumerable<Account> accountsPerPages = repository.Get(page, pageSize);
            //определяем информацию о номере, размере и общем количестве страниц для отображения на View
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Total };
            //создаем объект модели, с которым будет связан файл Index.cshtml (в этом файле @model = bla bla AccountIndexViewModel) по этой информации будет генерироваться View
            
            //класс PageLinks в файле PagingHelpers.cs на основании инфы и AccountIndexViewModel формирует ссылки между страницами на View

            AccountIndexViewModel ivm = new AccountIndexViewModel() { PageInfo = pageInfo, Accounts = accountsPerPages };
           
            
            
            return View(ivm);
        }
    }
}