using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(POPSMvcWcfApplication.Startup))]
namespace POPSMvcWcfApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
