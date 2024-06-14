using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Invetker.Startup))]
namespace Invetker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
