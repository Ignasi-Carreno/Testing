using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebLogin.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult AccessDenied()
        {
            Response.StatusCode = 403;
            return View();
        }
    }
}