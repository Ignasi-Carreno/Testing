using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebLogin.Site.Helpers
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CustomAuthorizeAttribute:AuthorizeAttribute
    {
        /// <summary>
        /// Return error 403 if user is autenticated doesn't have appropiate role
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpForbiddenResult();
            }
            else
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}