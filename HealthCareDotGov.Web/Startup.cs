using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HealthCareDotGov.Web.Startup))]
namespace HealthCareDotGov.Web
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
