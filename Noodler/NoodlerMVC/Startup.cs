using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NoodlerMVC.Startup))]
namespace NoodlerMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
