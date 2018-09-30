using System.Collections.Generic;

namespace WebLogin.Site.Models
{
    public class User
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public List<Role> Roles { get; set; }
    }
}