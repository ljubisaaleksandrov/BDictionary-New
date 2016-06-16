using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BDictionary.UI.Startup))]
namespace BDictionary.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
