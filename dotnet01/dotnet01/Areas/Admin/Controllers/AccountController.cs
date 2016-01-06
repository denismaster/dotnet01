using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using dotnet01.Areas.Admin.Models;
using System.Data.Entity;
using dotnet01.Areas.Admin.Controllers;
using Courses.Buisness;
using Models2 = Courses.Models;
namespace dotnet01.Areas.Admin.Controllers
{

    public class AccountController : Controller
    {
        IAccountRepository repository;

        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            repository = new AccountRepository();
            if (accountService == null)
                throw new ArgumentNullException();
            this.accountService = accountService;
        }

        [HttpGet]
        public ActionResult New()
        {
            Account account = new Account();
            AccountNewViewModel model = new AccountNewViewModel() { Account = account };
            return View(model);
        }

        [HttpPost]
        public ActionResult New(Account account)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (account.Mail == null)
                    {
                        account.Mail = "";
                    }
                    if (account.Role == null)
                    {
                        account.Role = "";
                    }
                    repository.Add(account);
                    repository.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                Log.Write(e);
                ModelState.AddModelError("", "Unable to save changes");
            }
            AccountNewViewModel nvm = new AccountNewViewModel() { Account = account };
            return View(nvm);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            Account account = repository.Get(id.Value);
            if (account == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            AccountEditViewModel model = new AccountEditViewModel() { Account = account };
            return View(model);
        }

        [HttpPost]
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

            AccountEditViewModel evm = new AccountEditViewModel() { Account = account };
            return View(evm);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            Account account = repository.Get(id.Value);
            if (account == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            AccountEditViewModel model = new AccountEditViewModel() { Account = account };
            return View(model);
        }

        public ActionResult Index(int page = 1)
        {

            //TODO : добавить два аргумента List<FieldFilter> и SortFilter, реализовать вызов перегруженного метода Get из репозитория
            //который осуществляет выборку из БД по заданным фильтрам
            //сигнатуру метода и его реализацию можно посмотреть в файле AccountRepository.cs
            //перегрузка Get с фильтрами не была тщательно протестирована, возможны ошибки
            //отредактировать представление для возможности переключения фильтров.
            //информация о передаче сложных аргументов типа List, Array в контроллер http://metanit.com/sharp/mvc5/5.11.php
            //TODO UPDATE. Используй новые классы, Люк. Ибо ViewModel будет меняться,так что возможно, что все пропадет, как и орден Джедаев.
            int pageSize = 3;
            int Total = repository.Count();
            //получаем страницу, заданную в параметре из базы данных 
            //IEnumerable<Account> accountsPerPages = repository.Get(page, pageSize);
            var accounts = accountService.GetAccounts(page,pageSize);
            //определяем информацию о номере, размере и общем количестве страниц для отображения на View
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Total };
            //создаем объект модели, с которым будет связан файл Index.cshtml (в этом файле @model = bla bla AccountIndexViewModel) по этой информации будет генерироваться View

            //класс PageLinks в файле PagingHelpers.cs на основании инфы и AccountIndexViewModel формирует ссылки между страницами на View

            AccountIndexViewModel ivm = new AccountIndexViewModel() { PageInfo = pageInfo, Accounts = accounts };

            return View(ivm);
        }

    }
}