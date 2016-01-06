using System.Collections.Generic;
using System.Web.Mvc;
using dotnet01.Areas.Manager.Models;
using dotnet01.Areas.Admin.Models;

namespace dotnet01.Areas.User.Controllers
{
    public class UserCourseController : Controller
    {
        AccountContext db = new AccountContext();

        public ActionResult Index()
        {
            IEnumerable<Course> courses = db.Courses;
            ViewBag.Courses = courses;
            return View();
        }

    }
}