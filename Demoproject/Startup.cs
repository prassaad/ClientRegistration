using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Demoproject.Startup))]
namespace Demoproject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
