using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
[assembly: OwinStartup(typeof(Courses.Gui.Manager.Startup))]

namespace Courses.Gui.Manager
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                CookieName = "CourseCookie",
                LoginPath = new PathString("/Account/Login"),
            });
        }
    }
}
