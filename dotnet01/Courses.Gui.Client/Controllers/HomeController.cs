﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Courses.Gui.Client.Controllers
{
    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult About()
        {
            return View();
        }
    }
}
