using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Courses.Gui.Manager
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ControllerBuilder.Current.SetControllerFactory(new ManagerControllerFactory());
            //DatabaseContext context = new DatabaseContext();
            //if (context.Database.Exists())
            //{
            //    // set the database to SINGLE_USER so it can be dropped
            //    context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "ALTER DATABASE [" + context.Database.Connection.Database + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");

            //    // drop the database
            //    context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "USE master DROP DATABASE [" + context.Database.Connection.Database + "]");
            //}

            //Database.SetInitializer(new DBInitializer());
            //context.Database.Initialize(true);
        }
    }
}
