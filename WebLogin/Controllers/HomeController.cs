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

        [CustomAuthorize(Roles = "PAGE_1, ADMIN")]
        public ActionResult Page1()
        {
            return View();
        }

        [CustomAuthorize(Roles = "PAGE_2, ADMIN")]
        public ActionResult Page2()
        {
            return View();
        }

        [CustomAuthorize(Roles = "PAGE_3, ADMIN")]
        public ActionResult Page3()
        {
            return View();
        }
    }
}