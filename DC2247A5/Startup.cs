using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DC2247A5.Startup))]

namespace DC2247A5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
