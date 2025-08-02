using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Eduegate.ERP.Admin.Startup))]
namespace Eduegate.ERP.Admin
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
