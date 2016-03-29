using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Courses.Gui.Client.Models;

namespace Courses.Gui.Client.Controllers
{
    //[Authorize(Roles = "Admin, Manager, Default")]
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
        public ActionResult Basket()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
     
    }
}

