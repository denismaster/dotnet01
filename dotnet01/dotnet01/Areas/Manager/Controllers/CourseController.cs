using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using dotnet01.Areas.Admin.Models;
using dotnet01.Areas.Manager.Models;
using System.Data.Entity;
using dotnet01.Areas.Admin.Controllers;
using Courses.Buisness.Account;
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
    }
}