using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(practiceA.Startup))]
namespace practiceA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
