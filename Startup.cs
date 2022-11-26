using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BoitMail.Startup))]
namespace BoitMail
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
