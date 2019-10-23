using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AmazonLite.WebUI.Startup))]
namespace AmazonLite.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
