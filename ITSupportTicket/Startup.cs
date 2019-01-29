using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ITSupportTicket.Startup))]
namespace ITSupportTicket
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
