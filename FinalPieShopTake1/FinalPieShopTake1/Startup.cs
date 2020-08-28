using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FinalPieShopTake1.Startup))]
namespace FinalPieShopTake1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
