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
using System.IO;
using System.Drawing;

namespace Courses.Gui.Manager.Controllers
{
    //[Authorize(Roles = "Manager,Admin")]
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
            var product = productService.GetProductWithAccauntsAndPartners(null);
            return View(product);
        }
        [HttpPost]
        public ActionResult New(ProductViewModelForAddEditView productView, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    productView.imagePath = SaveImageInFolder(file);
                    productService.Add(productView);
                    productService.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes");
            }
            return View(productView);
        }

        /// <summary>
        /// Сохраняет картинку в папке CoursesImages. Если картинка в запросе полученна не была, 
        /// то возвращает пустую строку, иначе возвращает путь к картинке
        /// </summary>
        /// <param name="file">Путь к картинке</param>
        /// <returns></returns>
        private String SaveImageInFolder(HttpPostedFileBase file)
        {
            string path = null;
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    string filename = System.IO.Path.GetFileName(file.FileName);
                    path = "~/CoursesImages/" + filename;
                    // file is uploaded
                    file.SaveAs(Server.MapPath(path));
                }
            }
            catch (Exception c)
            {
                return null;
            }
            return path;
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            var productView = productService.GetProductWithAccauntsAndPartners(id.Value);
            if (productView.Id == 0)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            return View(productView);
        }

        [HttpPost]
        public ActionResult Edit(ProductViewModelForAddEditView product, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(file != null)
                        product.imagePath = SaveImageInFolder(file);
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
        public ActionResult EditProductCategorys(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            var productView = productService.GetProductWithAllCategorys(id.Value);
            if (productView.Id == 0)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            return View(productView);
        }

        [HttpPost]
        public ActionResult EditProductCategorys(ProductWithAllCategorysViewModel product, IEnumerable<int> selectedCategorys)
        {
            try
            {
                productService.EditProductCategorys(product, (selectedCategorys != null)?selectedCategorys.ToArray() : null);
                return RedirectToAction("Details", new { id = product.Id});
            }
            catch(Exception e) 
            {
                ModelState.AddModelError("", "Unable to save changes");
            }
            var productView = productService.GetProductWithAllCategorys(product.Id);
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
            var product = productService.GetProductWithCurrentCategorys(id.Value);
            if (product == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            return View(product);
        }

        public ActionResult Index(string sortOrder, string CurrentNameFilter, string CurrentParentIdFilter, string SearchNameString, string SearchParentIdString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = (String.IsNullOrEmpty(sortOrder) || sortOrder == "Name") ? "NameDesc" : "Name";
            ViewBag.ActiveSortParam = sortOrder == "Active" ? "ActiveDesc" : "Active";
            ViewBag.TypeSortParam = sortOrder == "Type" ? "TypeDesc" : "Type";
            ViewBag.PartnerIdSortParam = sortOrder == "PartnerId" ? "PartnerIdDesc" : "PartnerId";

            if (Request.HttpMethod == "GET")
            {
                SearchNameString = CurrentNameFilter;
                SearchParentIdString = CurrentParentIdFilter;
            }
            else
            {
                page = 1;
            }
            ViewBag.CurrentNameFilter = SearchNameString;
            ViewBag.CurrentParentIdFilter = SearchParentIdString;

            var sortFilter = new Courses.Buisness.Filtering.SortFilter() { SortOrder = sortOrder };

            List<Buisness.Filtering.FieldFilter> fieldFilters = new List<FieldFilter>();
            if (!String.IsNullOrEmpty(SearchNameString))
            {
                FieldFilter fieldFilter = new FieldFilter() { Name = "Name", Value = SearchNameString.ToString() };
                fieldFilters.Add(fieldFilter);
            }
            if (!String.IsNullOrEmpty(SearchParentIdString))
            {
                FieldFilter fieldFilter = new FieldFilter() { Name = "PartnerID", Value = SearchParentIdString.ToString() };
                fieldFilters.Add(fieldFilter);
            }
            

            int pageSize = 3;
            int currentPage = page ?? 1;
            var products = productService.GetProducts(currentPage, pageSize, fieldFilters, sortFilter);
            return View(products);
        }
    }
}