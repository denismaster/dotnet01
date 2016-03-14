using Courses.Buisness.Filtering;
using Courses.Buisness.Services;
using Courses.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Courses.Gui.Manager.Controllers
{
    //[Authorize(Roles = "Manager,Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            if (categoryService == null)
                throw new ArgumentNullException();
            this.categoryService = categoryService;
        }
        [HttpGet]
        public ActionResult New()
        {
            var category = categoryService.GetCategoryWithCategorys(null);
            return View(category);
        }
        [HttpPost]
        public ActionResult New(CategoryViewModelForAddEditView categoryView)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    categoryService.Add(categoryView);
                    categoryService.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes");
            }
            return View(categoryView);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            var categoryView = categoryService.GetCategoryWithCategorys(id.Value);
            if (categoryView.Id == 0)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            return View(categoryView);
        }

        [HttpPost]
        public ActionResult Edit(CategoryViewModelForAddEditView category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    categoryService.Edit(category);
                    categoryService.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes");
            }
            return View(category);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            var category = categoryService.GetByID(id.Value);
            if (category == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            return View(category);
        }

        [HttpPost]
        public ActionResult Delete(CategoryViewModel category)
        {
            try
            {
                categoryService.Delete(category);
                categoryService.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes");
            }
            return View(category);
        }
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            var category = categoryService.GetByID(id.Value);
            if (category == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            return View(category);
        }

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = (String.IsNullOrEmpty(sortOrder) || sortOrder == "Name") ? "NameDesc" : "Name";
            ViewBag.ActiveSortParam = sortOrder == "Active" ? "ActiveDesc" : "Active";
            ViewBag.UpdateDateSortParam = sortOrder == "UpdateDate" ? "UpdateDateDesc" : "UpdateDate";

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
                FieldFilter fieldFilter = new FieldFilter() { Name = "Name", Value = searchString.ToString() };
                fieldFilters.Add(fieldFilter);
            }

            int pageSize = 3;
            int currentPage = page ?? 1;
            var categorys = categoryService.GetCategorys(currentPage, pageSize, fieldFilters, sortFilter);
            return View(categorys);
        }
    }
}