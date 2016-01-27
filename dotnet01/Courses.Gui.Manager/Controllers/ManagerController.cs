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

namespace Courses.Gui.Manager.Controllers
{
    public class ManagerController : Controller
    {
        private readonly ICourseService courseService;

        public ManagerController(ICourseService courseService)
        {
            if (courseService == null)
                throw new ArgumentNullException();
            this.courseService = courseService;
        }
        [HttpGet]
        public ActionResult New()
        {
            CourseViewModel model = new CourseViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult New(CourseViewModel course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    courseService.Add(course);
                    courseService.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Unable to save changes");
            }
            return View(course);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            var course = courseService.GetByID(id.Value);
            if (course == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            return View(course);
        }

        [HttpPost]
        public ActionResult Edit(CourseViewModel course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    courseService.Edit(course);
                    courseService.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Unable to save changes");
            }
            return View(course);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            var course = courseService.GetByID(id.Value);
            if (course == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            return View(course);
        }

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParam = (String.IsNullOrEmpty(sortOrder) || sortOrder == "Title") ? "TitleDesc" : "Title";
            ViewBag.DatesSortParam = sortOrder == "Dates" ? "DatesDesc" : "Dates";
            ViewBag.OrganizerSortParam = sortOrder == "Organizer" ? "OrganizerDesc" : "Organizer";
            ViewBag.ActiveSortParam = sortOrder == "Active" ? "ActiveDesc" : "Active";

            if (Request.HttpMethod == "GET")
            {
                searchString = currentFilter;
            }
            else
            {
                page = 1;
            }
            ViewBag.CurrentFilter = searchString;

            SortFilter sortFilter = new SortFilter() { SortOrder = sortOrder };
            List<Buisness.Filtering.FieldFilter> fieldFilters = new List<FieldFilter>();
            if (!String.IsNullOrEmpty(searchString))
            {
                FieldFilter fieldFilter = new FieldFilter() { Name = "Title", Value = searchString };
                fieldFilters.Add(fieldFilter);
            }

            int pageSize = 3;
            int currentPage = page ?? 1;
            var courses = courseService.GetCourses(currentPage, pageSize, fieldFilters, sortFilter);
            return View(courses);
        }
    }
}