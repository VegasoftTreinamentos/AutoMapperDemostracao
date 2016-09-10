using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AutoMapper.Demo.Startup))]
namespace AutoMapper.Demo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
