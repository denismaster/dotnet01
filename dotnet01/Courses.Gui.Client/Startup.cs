using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(Courses.Gui.Client.Startup))]

namespace Courses.Gui.Client
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Account/Login"),
            });
        }
    }
}
