using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MasraEmas.Startup))]
namespace MasraEmas
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
