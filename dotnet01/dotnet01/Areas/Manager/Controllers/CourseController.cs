using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using dotnet01.Areas.Admin.Models;
using dotnet01.Areas.Manager.Models;
using System.Data.Entity;
using Courses.Buisness;
using Models2 = Courses.Models;


namespace dotnet01.Areas.Manager.Controllers
{
    public class CourseController : Controller
    {
        AccountContext db = new AccountContext();
        public ActionResult Index()
        {
            IEnumerable<Course> courses = db.Courses;
            ViewBag.Courses = courses;
            return View();
        }

        public ActionResult ManagerPage()
        {
            IEnumerable<Course> courses = db.Courses;
            ViewBag.Courses = courses;
            return View();
        }

        [HttpGet]
        public ActionResult New()
        {
           Course course = new Course();
           CourseNewViewModel model = new CourseNewViewModel() { Course=course };
            return View(model);
        }
        [HttpPost]
        public ActionResult New(Course course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (course.Dates == null)
                    {
                        course.Dates = "";
                    }
                    if (course.Location == null)
                    {
                        course.Location = "";
                    }
                    if (course.Teacher == null)
                    {
                        course.Teacher = "";
                    }
                    if (course.Organizer == null)
                    {
                        course.Organizer = "";
                    }
                    if (course.Description == null)
                    {
                        course.Description = "";
                    }
                    if (course.Image == null)
                    {
                        course.Image = null;
                    }
                    db.Courses.Add(course);
                    db.SaveChanges();
                    return RedirectToAction("ManagerPage");

                }
            }
            catch (Exception e)
            {
                Log.Write(e);
                ModelState.AddModelError("", "Unable to save changes");
            }
            return View();
        }

    }
}