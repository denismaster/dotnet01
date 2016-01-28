using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using dotnet01.Areas.Admin.Models;
using Ninject;
using Courses.DAL;
using Courses.Models;
namespace dotnet01
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            MainContext main = new MainContext();
            main.Database.CreateIfNotExists();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //Let's do magic with ninject
            ControllerBuilder.Current.SetControllerFactory(new AdminControllerFactory());
        }
    }
}
