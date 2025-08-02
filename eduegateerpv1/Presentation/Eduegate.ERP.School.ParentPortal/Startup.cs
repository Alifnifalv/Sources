using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Eduegate.ERP.School.Portal.Startup))]
namespace Eduegate.ERP.School.Portal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
