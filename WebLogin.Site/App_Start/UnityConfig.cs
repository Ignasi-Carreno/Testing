using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using WebLogin.BLL;
using WebLogin.DAL;
using WebLogin.IBLL;
using WebLogin.IDAL;

namespace WebLogin.Site
{
    public static class UnityConfig
    {
        /// <summary>
        /// Register types to be resolved
        /// </summary>
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IUserModel, UserModel>();
            container.RegisterType<IUserDAL, UserDAL>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}