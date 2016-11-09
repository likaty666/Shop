using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Shop.UI.Startup))]
namespace Shop.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
