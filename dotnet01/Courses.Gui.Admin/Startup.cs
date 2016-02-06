using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Courses.Gui.Admin.Startup))]

namespace Courses.Gui.Admin
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
