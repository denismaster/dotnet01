using Courses.DAL;
using System.Data.Entity;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Courses.Gui.Client
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext,Configuration>());

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ControllerBuilder.Current.SetControllerFactory(new ClientControllerFactory());
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



            //тест фильтрации 
            /* AccountFilterFactory factory = new AccountFilterFactory();
             FieldFilter filter = new FieldFilter();
             filter.Name = "LogIn";
             filter.Value = "K";
             FieldFilter filter2 = new FieldFilter();
             filter2.Name = "Role";
             filter2.Value = "Manager";
             List<FieldFilter> filters = new List<FieldFilter>();
             filters.Add(filter);
             filters.Add(filter2);
             Func<User, bool> func = factory.GetFilterExpression(filters);
             IAccountRepository accRep = new AccountRepository();
             IEnumerable<User> users =  accRep.Get(1, 3, func);
                */

        }
    }
}
