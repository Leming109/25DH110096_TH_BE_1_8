using System.Web;
using System.Web.Mvc;

namespace _25DH110096_TH_BE_1_8
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
