using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Courses.Buisness;
using Courses.Models;
using Courses.Models.Repositories;
using Courses.ViewModels;
using Courses.Buisness.Filtering;
using Courses.Buisness.Services;

namespace Courses.Gui.Manager.Controllers
{
    public class ManagerController : Controller
    {
        private readonly IProductService productService;

        public ManagerController(IProductService productService)
        {
            if (productService == null)
                throw new ArgumentNullException();
            this.productService = productService;
        }
        [HttpGet]
        public ActionResult New()
        {
            ProductViewModel model = new ProductViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult New(ProductViewModel product)
        {
            try
            {
                
                if (ModelState.IsValid)
                {
                    productService.Add(product);
                    productService.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes");
            }
            return View(product);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            var product = productService.GetById(id.Value);
            if (product == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(ProductViewModel product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    productService.Edit(product);
                    productService.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes");
            }
            return View(product);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            var product = productService.GetById(id.Value);
            if (product == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            return View(product);
        }

        [HttpPost]
        public ActionResult Delete(ProductViewModel product)
        {
            try
            {
                    productService.Delete(product);
                    productService.SaveChanges();
                    return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes");
            }
            return View(product);
        }
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            var product = productService.GetById(id.Value);
            if (product == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            return View(product);
        }

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = (String.IsNullOrEmpty(sortOrder) || sortOrder == "Name") ? "NameDesc" : "Name";
            ViewBag.ActiveSortParam = sortOrder == "Active" ? "ActiveDesc" : "Active";
            ViewBag.TypeSortParam = sortOrder == "Type" ? "TypeDesc" : "Type";
            ViewBag.PartnerIdSortParam = sortOrder == "PartnerId" ? "PartnerIdDesc" : "PartnerId";

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
            var products = productService.GetProducts(currentPage, pageSize, fieldFilters, sortFilter);
            return View(products);
        }
    }
}