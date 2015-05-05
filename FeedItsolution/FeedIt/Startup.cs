using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FeedIt.Startup))]
namespace FeedIt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
