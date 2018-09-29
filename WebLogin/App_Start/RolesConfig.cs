using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace WebLogin
{
    public class RolesConfig
    {
        public static void RegisterRoles()
        {
            if (Roles.Enabled)
            {
                if (!Roles.RoleExists("ADMIN"))
                {
                    Roles.CreateRole("ADMIN");
                }

                if (!Roles.RoleExists("PAGE_1"))
                {
                    Roles.CreateRole("PAGE_1");
                }

                if (!Roles.RoleExists("PAGE_2"))
                {
                    Roles.CreateRole("PAGE_2");
                }

                if (!Roles.RoleExists("PAGE_3"))
                {
                    Roles.CreateRole("PAGE_3");
                }
            }
        }
    }
}
