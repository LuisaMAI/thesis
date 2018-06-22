using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Thesis_Datenbank_Aufgabe.Startup))]
namespace Thesis_Datenbank_Aufgabe
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
