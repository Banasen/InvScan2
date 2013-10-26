using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InvScan.Startup))]
namespace InvScan
{
    public partial class Startup 
    {
        public void Configuration(IAppBuilder app) 
        {
            ConfigureAuth(app);
        }
    }
}
