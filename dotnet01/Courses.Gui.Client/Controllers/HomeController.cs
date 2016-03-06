using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Courses.Gui.Client.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Spa()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
       

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
