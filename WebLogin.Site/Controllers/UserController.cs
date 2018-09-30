using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebLogin.IBLL;
using WebLogin.Site.Models;

namespace WebLogin.Site.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserModel userModel;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userModel"></param>
        public UserController(IUserModel userModel)
        {
            this.userModel = userModel;
        }

        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserViewModel user, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (IsValid(user))
                {
                    var userRoles = userModel.GetUser(user.UserName).Roles.Select(role => AutoMapper.Mapper.Map<Role>(role));
                    foreach (var role in userRoles)
                    {
                        if (!Roles.IsUserInRole(user.UserName, role.ToString()))
                            Roles.AddUserToRole(user.UserName, role.ToString());
                    }

                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }
            return View(user);
        }

        public ActionResult Logout()
        {
            var roles = Roles.GetRolesForUser(User.Identity.Name);
            Roles.RemoveUserFromRoles(User.Identity.Name, roles);
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public bool IsValid(UserViewModel user)
        {
            return userModel.IsValidUser(user.UserName, user.Password);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}