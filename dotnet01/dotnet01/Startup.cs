using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(dotnet01.Startup))]
namespace dotnet01
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
