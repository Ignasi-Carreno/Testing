using System.Web.Mvc;
using WebLogin.Helpers;

namespace WebLogin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [CustomAuthorize(Roles = "PAGE_1")]
        public ActionResult Page1()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [CustomAuthorize(Roles = "PAGE_2")]
        public ActionResult Page2()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}