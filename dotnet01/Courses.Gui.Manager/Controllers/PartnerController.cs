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

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = (String.IsNullOrEmpty(sortOrder) || sortOrder == "Name") ? "NameDesc" : "Name";
            ViewBag.AddressSortParam = sortOrder == "Address" ? "AddressDesc" : "Address";
            ViewBag.PhoneSortParam = sortOrder == "Phone" ? "PhoneDesc" : "Phone";
            ViewBag.EmailSortParam = sortOrder == "Email" ? "EmailDesc" : "Email";

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