using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebLogin.Site.Helpers;

namespace WebLogin.Site
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RolesConfig.RegisterRoles();
            AutoMapperConfig.RegisterMappings();
            UnityConfig.RegisterComponents();

            // API authorization registration.
            GlobalConfiguration.Configuration.MessageHandlers.Add(new AuthorizationHeaderHandler());
        }
    }
}
