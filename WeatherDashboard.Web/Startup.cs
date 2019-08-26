using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WeatherDashboard.Web.Startup))]
namespace WeatherDashboard.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
