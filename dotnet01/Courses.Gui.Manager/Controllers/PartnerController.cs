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
    public class PartnerController: Controller
    {
        private readonly IPartnerService partnerService;

        public PartnerController(IPartnerService partnerService)
        {
            if (partnerService == null)
                throw new ArgumentNullException();
            this.partnerService = partnerService;
        }

        [HttpGet]
        public ActionResult New()
        {
            PartnerViewModel model = new PartnerViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult New(PartnerViewModel partner)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    partnerService.Add(partner);
                    partnerService.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes");
            }
            return View(partner);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            var pather = partnerService.GetByID(id.Value);
            if (pather == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            return View(pather);
        }

        [HttpPost]
        public ActionResult Edit(PartnerViewModel partner)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    partnerService.Edit(partner);
                    partnerService.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Unable to save changes");
            }
            return View(partner);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            var partner = partnerService.GetByID(id.Value);
            if (partner == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            return View(partner);
        }

        [HttpPost]
        public ActionResult Delete(PartnerViewModel partner)
        {
            try
            {
                partnerService.Delete(partner);
                partnerService.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes");
            }
            return View(partner);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            var account = partnerService.GetByID(id.Value);
            if (account == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            return View(account);
        }

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = (String.IsNullOrEmpty(sortOrder) || sortOrder == "Name") ? "NameDesc" : "Name";
            ViewBag.CreatedDateSortParam = sortOrder == "CreatedDate" ? "CreatedDateDesc" : "CreatedDate";


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
            var partners = partnerService.GetPartners(currentPage, pageSize, fieldFilters, sortFilter);
            return View(partners);
        }

    }
}