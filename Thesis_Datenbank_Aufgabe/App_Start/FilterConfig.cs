using System.Web;
using System.Web.Mvc;

namespace Thesis_Datenbank_Aufgabe
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
