﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using dotnet01.Areas.Admin.Models;
using dotnet01.Areas.Admin.Controllers;



namespace dotnet01
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            Test test = new Test();
            test.start();
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces:new string[] { "dotnet01.Controllers"}
            );
        }
    }
}
