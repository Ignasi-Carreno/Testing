using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace WebLogin.Site
{
    public class RolesConfig
    {
        /// <summary>
        /// Register the avalaiable roles
        /// </summary>
        public static void RegisterRoles()
        {
            if (Roles.Enabled)
            {
                if (!Roles.RoleExists(Models.Role.ADMIN.ToString()))
                {
                    Roles.CreateRole(Models.Role.ADMIN.ToString());
                }

                if (!Roles.RoleExists(Models.Role.PAGE_1.ToString()))
                {
                    Roles.CreateRole(Models.Role.PAGE_1.ToString());
                }

                if (!Roles.RoleExists(Models.Role.PAGE_2.ToString()))
                {
                    Roles.CreateRole(Models.Role.PAGE_2.ToString());
                }

                if (!Roles.RoleExists(Models.Role.PAGE_3.ToString()))
                {
                    Roles.CreateRole(Models.Role.PAGE_3.ToString());
                }
            }
        }
    }
}
