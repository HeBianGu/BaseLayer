using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HeBianGu.Product.MovieServer.Startup))]
namespace HeBianGu.Product.MovieServer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
