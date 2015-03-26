using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Pathfinder.Startup))]
namespace Pathfinder
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
