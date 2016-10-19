using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_4952.Startup))]
namespace _4952
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
