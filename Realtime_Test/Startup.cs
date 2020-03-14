using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Realtime_Test.Startup))]
namespace Realtime_Test
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
