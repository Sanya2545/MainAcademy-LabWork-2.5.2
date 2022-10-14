using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CRM_v2.Startup))]
namespace CRM_v2
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
