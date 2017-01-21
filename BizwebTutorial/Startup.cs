using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BizwebTutorial.Startup))]
namespace BizwebTutorial
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
