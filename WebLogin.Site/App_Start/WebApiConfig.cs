using System.Web.Http;
using Unity;
using WebLogin.BLL;
using WebLogin.DAL;
using WebLogin.IBLL;
using WebLogin.IDAL;

namespace WebLogin.Site
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Dependency injection
            var container = new UnityContainer();
            container.RegisterType<IUserDAL, UserDAL>();
            container.RegisterType<IUserModel, UserModel>();
            config.DependencyResolver = new UnityResolver(container);

            // Routes of API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
