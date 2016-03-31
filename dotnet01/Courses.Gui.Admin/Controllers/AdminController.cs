using Courses.Buisness;
using Courses.Buisness.Filtering;
using Courses.Buisness.Services;
using Courses.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
namespace Courses.Gui.Admin.Controllers
{
    //[Authorize(Roles="Admin")]
    public class AdminController : Controller
    {
        private readonly IAccountService accountService;

        private readonly IPasswordHasher hasher;

        public AdminController(IAccountService accountService, IPasswordHasher hasher)
        {
            if (accountService == null)
                throw new ArgumentNullException("accountService");
            if (hasher == null)
                throw new ArgumentNullException("hasher");
            this.accountService = accountService;
            this.hasher = hasher;
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
                    account.Password = hasher.Hash(account.Password);
                    accountService.Add(account);
                    accountService.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Unable to save changes:" + e.Message);
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
            var items = new List<SelectListItem>();
            return View(account);
        }

        [HttpPost]
        public ActionResult Edit(AccountViewModel account)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    account.Password = hasher.Hash(account.Password);
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
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            var account = accountService.GetByID(id.Value);
            if (account == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            return View(account);
        }

        [HttpPost]
        public ActionResult Delete(AccountViewModel account)
        {
            try
            {
                accountService.Delete(account);
                accountService.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
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

            var sortFilter = new Courses.Buisness.Filtering.SortFilter() { SortOrder = sortOrder };
            List<Buisness.Filtering.FieldFilter> fieldFilters = new List<FieldFilter>();
            if (!String.IsNullOrEmpty(searchString))
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