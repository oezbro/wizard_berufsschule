using System.Web;
using System.Web.Mvc;

namespace Wizard_Aydin_Olga
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
