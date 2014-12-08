using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TwitBook.Startup))]
namespace TwitBook
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
