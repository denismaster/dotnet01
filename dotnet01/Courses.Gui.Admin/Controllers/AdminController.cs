using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Courses.Buisness;
using Courses.Models;
using Courses.ViewModels;
using Courses.Buisness.Filtering;

namespace Courses.Gui.Admin.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAccountService accountService;

        public AdminController(IAccountService accountService)
        {
            if (accountService == null)
                throw new ArgumentNullException();
            this.accountService = accountService;
        }
        [HttpGet]
        public ActionResult New()
        {
            AccountViewModel model = new AccountViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult New(AccountViewModel account)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    accountService.Add(account);
                    accountService.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Unable to save changes");
            }
            return View(account);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            var account = accountService.GetByID(id.Value);
            if (account == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            return View(account);
        }

        [HttpPost]
        public ActionResult Edit(AccountViewModel account)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    accountService.Edit(account);
                    accountService.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Unable to save changes");
            }
            return View(account);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            var account = accountService.GetByID(id.Value);
            if (account == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            return View(account);
        }

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            //TODO : добавить два аргумента List<FieldFilter> и SortFilter, реализовать вызов перегруженного метода GetAccounts из сервиса
            //который осуществляет выборку из БД по заданным фильтрам
            //сигнатуру метода и его реализацию можно посмотреть в файле AccountaccountService.cs
            //перегрузка Get с фильтрами не была тщательно протестирована, возможны ошибки
            //отредактировать представление для возможности переключения фильтров.
            //информация о передаче сложных аргументов типа List, Array в контроллер http://metanit.com/sharp/mvc5/5.11.php
            ViewBag.CurrentSort = sortOrder; 
            ViewBag.LoginSortParam = (String.IsNullOrEmpty(sortOrder) || sortOrder == "LogIn") ? "LogInDesc" : "LogIn";
            ViewBag.RoleSortParam = sortOrder == "Role" ? "RoleDesc" : "Role";

            if (Request.HttpMethod == "GET")
            {
                searchString = currentFilter;
            }
            else
            {
                page = 1;
            }
            ViewBag.CurrentFilter = searchString;

            Buisness.Filtering.SortFilter sortFilter = new SortFilter() { SortOrder = sortOrder };
            List<Buisness.Filtering.FieldFilter> fieldFilters = new List<FieldFilter>();
            if(!String.IsNullOrEmpty(searchString))
            {
                FieldFilter fieldFilter = new FieldFilter() { Name = "LogIn", Value = searchString };
                fieldFilters.Add(fieldFilter);
            }

            int pageSize = 3;
            int currentPage = page ?? 1;
            var accounts = accountService.GetAccounts(currentPage, pageSize, fieldFilters, sortFilter);
            return View(accounts);
        }
    }
}