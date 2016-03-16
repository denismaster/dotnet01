using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
[assembly: OwinStartup(typeof(Courses.Gui.Admin.Startup))]

namespace Courses.Gui.Admin
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
