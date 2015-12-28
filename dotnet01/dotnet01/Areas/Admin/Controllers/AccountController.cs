﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using dotnet01.Areas.Admin.Models;
using System.Data.Entity;
using dotnet01.Areas.Admin.Controllers;
using System.Data;
using dotnet01.Validation;
namespace dotnet01.Areas.Admin.Controllers
{

    public class AccountController : Controller
    {
        IAccountRepository repository;

        public AccountController()
        {
            repository = new AccountRepository();
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
            AccountValidation valid = new AccountValidation();
            try
            
                {

                    if (ModelState.IsValid)
                    {
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

        public ActionResult IndexSort(int page = 1, String sorting = "LogIn")
        {
            int pageSize = 3;
            int totalAccounts = repository.Count();
            IEnumerable<Account> sortedAccountsPerPage = repository.Get(page, pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = totalAccounts
            };

            switch (sorting)
            {
                case "LogInDesc":
                    sortedAccountsPerPage.OrderByDescending(acc => acc.Login);
                    break;
                case "Mail":
                    sortedAccountsPerPage.OrderBy(acc => acc.Mail);
                    break;
                case "MailDesc":
                    sortedAccountsPerPage.OrderByDescending(acc => acc.Mail);
                    break;
                case "Role":
                    sortedAccountsPerPage.OrderBy(acc => acc.Role);
                    break;
                case "RoleDesc":
                    sortedAccountsPerPage.OrderByDescending(acc => acc.Role);
                    break;
                default: 
                    sortedAccountsPerPage.OrderBy(acc => acc.Login);
                    break;
               
            }

            AccountIndexViewModel accountIndexViewModel = new AccountIndexViewModel()
            {
                PageInfo = pageInfo,
                Accounts = sortedAccountsPerPage
            };
            return View(accountIndexViewModel);
        }

        public ActionResult IndexFilter(int page = 1, String filterField = "Role", String filterValue = "")
        {
            int pageSize = 3;
            int totalAccounts = repository.Count();
            IEnumerable<Account> filteredAccountsPerPage;

            switch (filterField)
            {
                case "LogIn":
                    filteredAccountsPerPage = repository.Get(page, pageSize, acc => acc.Login == filterValue);
                    break;
                case "Role":
                    filteredAccountsPerPage = repository.Get(page, pageSize, acc => acc.Role == filterValue);
                    break;
                case "Mail":
                    filteredAccountsPerPage = repository.Get(page, pageSize, acc => acc.Mail == filterValue);
                    break;
                default:
                    filteredAccountsPerPage = repository.Get(page, pageSize);
                    break;
            }

            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = totalAccounts
            };
            AccountIndexViewModel accountIndexViewModel = new AccountIndexViewModel()
            {
                PageInfo = pageInfo,
                Accounts = filteredAccountsPerPage
            };
            return View(accountIndexViewModel);
        }


    }
}