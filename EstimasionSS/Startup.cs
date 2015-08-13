using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EstimasionSS.Startup))]
namespace EstimasionSS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
